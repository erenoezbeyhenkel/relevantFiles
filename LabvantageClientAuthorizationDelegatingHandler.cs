using Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;
using Hcb.Rnd.Pwn.Application.Common.Constants;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Email;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.KeyVault;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Hcb.Rnd.Pwn.Infrastructure.DelegatingHandlers.Authorization;

public sealed class LabvantageClientAuthorizationDelegatingHandler(
    IOptions<HugoOptions> hugoOptions,
    HybridCache hybridCache,
    IEmailService emailService,
    IHttpClientFactory httpClientFactory,
    ICertificateCacheService certificateCacheService
) : DelegatingHandler
{
    /// <summary>
    /// This method is used to set the authorization header for the LabVantage API requests and to get a new connectionId if needed. 
    /// Additionally, it caches the connectionId in a hybrid cache.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var cacheOptions = new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromMinutes(30),
            LocalCacheExpiration = TimeSpan.FromMinutes(30)
        };

        string? connectionId = await hybridCache.GetOrCreateAsync(
            HybridCacheConstants.LabvantageConnnectionId,
            async _ => await GetNewConnectionIdAsync(cancellationToken),
            cancellationToken: cancellationToken,
            options: cacheOptions
        );

        if (!await ValidateConnectionIdAsync(connectionId, cancellationToken))
        {
            connectionId = await GetNewConnectionIdAsync(cancellationToken);
            await hybridCache.SetAsync(
                HybridCacheConstants.LabvantageConnnectionId,
                connectionId,
                cacheOptions,
                null,
                cancellationToken);
        }

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";

        request.RequestUri = new Uri($"{hugoOptions.Value.ApiUrl}{request.RequestUri.PathAndQuery}");
        request.Headers.Authorization = new AuthenticationHeaderValue("token", connectionId);
        request.Headers.Add("Cookie", $"connectionid={Uri.EscapeDataString(connectionId)}");
        request.Headers.UserAgent.ParseAdd($"pwn/{version} ({environment})");

        var response = await base.SendAsync(request, cancellationToken);

        
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var msg = new StringBuilder();

            msg.AppendLine("<h3 style='color:red;'>❌ LabVantage Request Failed</h3><br>");
            msg.AppendLine($"<b>Environment:</b> {environment}<br>");
            msg.AppendLine($"<b>Version:</b> {version}<br><br>");
            msg.AppendLine($"<b>Request URL:</b> {request.RequestUri}<br>");
            msg.AppendLine($"<b>Status Code:</b> {response.StatusCode}<br><br>");

            msg.AppendLine("<b>Request Headers:</b><br>");
            foreach (var header in request.Headers)
                msg.AppendLine($"- {header.Key}: {string.Join(", ", header.Value)}<br>");

            msg.AppendLine("<br><b>Response Headers:</b><br>");
            foreach (var header in response.Headers)
                msg.AppendLine($"- {header.Key}: {string.Join(", ", header.Value)}<br>");

            msg.AppendLine("<br><b>Response Body:</b><br>");
            msg.AppendLine($"{content}<br>");

            await emailService.SendEmail(msg.ToString());
        }

        return response;
    }



    /// <summary>
    /// This method is used to get a new connectionId from the LabVantage API.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    /// <exception cref="InvalidOperationException"></exception>


    private async Task<string> GetNewConnectionIdAsync(CancellationToken cancellationToken)
    {
        using var client = httpClientFactory.CreateClient("LabVantageCertClient");

        var payload = new
        {
            databaseid = hugoOptions.Value.DatabaseId,
            username = hugoOptions.Value.UserName,
            password = hugoOptions.Value.Password
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/labvantage/rest/connections", content, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        var cert = await certificateCacheService.GetCertificateAsync(hugoOptions.Value.CertificateName, cancellationToken);



        if (!response.IsSuccessStatusCode)
        {
            var msg = new StringBuilder();

            msg.AppendLine("🔄 <b>LabVantage Connection Request</b><br>");
            msg.AppendLine($"Request URL: {client.BaseAddress}/labvantage/rest/connections<br>");
            msg.AppendLine("<u>Headers:</u><br>");
            foreach (var header in client.DefaultRequestHeaders)
            {
                msg.AppendLine($"- {header.Key}: {string.Join(", ", header.Value)}<br>");
            }
            msg.AppendLine("<br>");

            msg.AppendLine("<u>📦 Payload:</u><br>");
            msg.AppendLine(JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true }).Replace("\n", "<br>").Replace("  ", "&nbsp;&nbsp;"));
            msg.AppendLine("<br>");

            msg.AppendLine("<u>🔐 Certificate Info:</u><br>");
            msg.AppendLine($"- Subject: {cert.Subject}<br>");
            msg.AppendLine($"- Thumbprint: {cert.Thumbprint}<br>");
            msg.AppendLine($"- Valid From: {cert.NotBefore}<br>");
            msg.AppendLine($"- Valid To: {cert.NotAfter}<br>");
            msg.AppendLine($"- Issuer: {cert.Issuer}<br><br>");

            msg.AppendLine("<u>📥 Response:</u><br>");
            msg.AppendLine($"Status Code: {response.StatusCode}<br>");
            msg.AppendLine($"{responseBody}<br><br>");
            msg.Insert(0, "<h3 style='color:red;'>❌ FAILED to retrieve LabVantage connection ID</h3><br>");
            await emailService.SendEmail(msg.ToString());

            throw new HttpRequestException($"Failed to get connectionId: {response.StatusCode}");
        }


        using var jsonDoc = JsonDocument.Parse(responseBody);
        return jsonDoc.RootElement
                      .GetProperty("connections")
                      .GetProperty("connectionid")
                      .GetString()
               ?? throw new InvalidOperationException("No connectionid in response");
    }



    /// <summary>
    /// This method is used to validate the connectionId by sending a GET request to the LabVantage API. 
    /// </summary>
    /// <param name="connectionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<bool> ValidateConnectionIdAsync(string? connectionId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(connectionId))
            return false;

        using var client = httpClientFactory.CreateClient("LabVantageCertClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", connectionId);
        client.DefaultRequestHeaders.Add("Cookie", $"connectionid={Uri.EscapeDataString(connectionId)}");

        var request = new HttpRequestMessage(HttpMethod.Get, "/labvantage/rest/connections")
        {
            Content = new StringContent("", Encoding.UTF8, "text/plain")
        };

        var response = await client.SendAsync(request, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        var cert = await certificateCacheService.GetCertificateAsync(hugoOptions.Value.CertificateName, cancellationToken);


        if (!response.IsSuccessStatusCode)
        {
            var msg = new StringBuilder();
            msg.AppendLine("🔄 <b>LabVantage Validate Connection ID</b><br>");
            msg.AppendLine($"Request URL: {client.BaseAddress}/labvantage/rest/connections<br>");
            msg.AppendLine("<u>Headers:</u><br>");
            foreach (var header in client.DefaultRequestHeaders)
            {
                msg.AppendLine($"- {header.Key}: {string.Join(", ", header.Value)}<br>");
            }
            msg.AppendLine("<br>");

            msg.AppendLine("<u>🔐 Certificate Info:</u><br>");
            msg.AppendLine($"- Subject: {cert.Subject}<br>");
            msg.AppendLine($"- Thumbprint: {cert.Thumbprint}<br>");
            msg.AppendLine($"- Valid From: {cert.NotBefore}<br>");
            msg.AppendLine($"- Valid To: {cert.NotAfter}<br>");
            msg.AppendLine($"- Issuer: {cert.Issuer}<br><br>");

            msg.AppendLine("<u>🆔 Connection ID:</u><br>");
            msg.AppendLine($"{connectionId}<br><br>");

            msg.AppendLine("<u>📥 Response:</u><br>");
            msg.AppendLine($"Status Code: {response.StatusCode}<br>");
            msg.AppendLine($"{responseBody}<br><br>");

            msg.Insert(0, "<h3 style='color:red;'>❌ FAILED to validate LabVantage connection ID</h3><br>");
            await emailService.SendEmail(msg.ToString());
            return false;
        }

        return true;
    }

}
