using Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;
using Hcb.Rnd.Pwn.Application.Common.Constants;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.Email;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.HttpClients;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.KeyVault;
using Hcb.Rnd.Pwn.Infrastructure.DelegatingHandlers.Authorization;
using Hcb.Rnd.Pwn.Infrastructure.Services.HttpClients;
using Hcb.Rnd.Pwn.Infrastructure.Services.KeyVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Security.Authentication;
using System.Text;

namespace Hcb.Rnd.Pwn.Infrastructure.DI;
////Http client configuration for client services. Each services should configure their own settings.
////https://www.youtube.com/watch?v=g-JGay_lnWI&ab_channel=MilanJovanovi%C4%87
public static class HttpClientsDependencyInjection
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services, ConfigurationManager configuration)
    {
        // Register reusable services
        services.AddSingleton<ICertificateCacheService, CertificateCacheService>();
        services.AddTransient<LabvantageClientAuthorizationDelegatingHandler>();
        services.AddTransient<HdpClientAuthorizationDelegatingHandler>();


        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";
        var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";


        var hugoOptions = configuration.GetSection(OptionConstants.HugoOptions).Get<HugoOptions>();

        // LabvantageCertClient (used for /connections)
        services.AddHttpClient("LabVantageCertClient", (serviceProvider, client) =>
        {
            var hugoOptions = serviceProvider.GetRequiredService<IOptions<HugoOptions>>();
            client.BaseAddress = new Uri(hugoOptions.Value.ApiUrl);
            client.DefaultRequestHeaders.UserAgent.ParseAdd($"pwn/{version} ({environment})");
            client.Timeout = Timeout.InfiniteTimeSpan;
        })
        .ConfigurePrimaryHttpMessageHandler(serviceProvider =>
        {
            var certCacheService = serviceProvider.GetRequiredService<ICertificateCacheService>();
            var hugoOptions = serviceProvider.GetRequiredService<IOptions<HugoOptions>>();
            var emailService = serviceProvider.GetRequiredService<IEmailService>();
            var cancellationToken = CancellationToken.None;

            try
            {
                var cert = certCacheService
                    .GetCertificateAsync(hugoOptions.Value.CertificateName, cancellationToken)
                    .GetAwaiter().GetResult();

                var handler = new HttpClientHandler
                {
                    SslProtocols = SslProtocols.Tls12,
                    ClientCertificateOptions = ClientCertificateOption.Manual
                };

                handler.ClientCertificates.Add(cert);
                handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                return handler;
            }
            catch (Exception ex)
            {
                emailService.SendEmail($"Failed to load LabVantageCertClient certificate:\n{ex.Message}\n{ex.StackTrace}").GetAwaiter().GetResult();
                throw;
            }
        });

        // ILabvantageHttpClientService (uses handler)
        services.AddHttpClient<ILabvantageHttpClientService, LabvantageHttpClientService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(hugoOptions.ApiUrl);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"pwn/{version} ({environment})");
            httpClient.Timeout = Timeout.InfiniteTimeSpan;
        })
        .ConfigurePrimaryHttpMessageHandler(serviceProvider =>
        {
            var certCacheService = serviceProvider.GetRequiredService<ICertificateCacheService>();
            var hugoOptions = serviceProvider.GetRequiredService<IOptions<HugoOptions>>();
            var emailService = serviceProvider.GetRequiredService<IEmailService>();
            var cancellationToken = CancellationToken.None;

            try
            {
                var cert = certCacheService
                    .GetCertificateAsync(hugoOptions.Value.CertificateName, cancellationToken)
                    .GetAwaiter().GetResult();

                var handler = new HttpClientHandler
                {
                    SslProtocols = SslProtocols.Tls12,
                    ClientCertificateOptions = ClientCertificateOption.Manual
                };

                handler.ClientCertificates.Add(cert);
                handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                return handler;
            }
            catch (Exception ex)
            {
                emailService.SendEmail($"Failed to load ILabvantageHttpClientService certificate:\n{ex.Message}\n{ex.StackTrace}").GetAwaiter().GetResult();
                throw;
            }
        })
        .AddHttpMessageHandler<LabvantageClientAuthorizationDelegatingHandler>()
        .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        // HDP HttpClient
        var hdpOptions = configuration.GetSection(OptionConstants.HdpOptions).Get<HdpOptions>();

        services.AddHttpClient<IHdpHttpClientService, HdpHttpClientService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(hdpOptions.ApiUrl);
            httpClient.Timeout = Timeout.InfiniteTimeSpan;
        })
        .AddHttpMessageHandler<HdpClientAuthorizationDelegatingHandler>()
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler();
        })
        .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        //WashAi Http Client

        var washAiOptions = configuration.GetSection(OptionConstants.WashAiOptions).Get<WashAiOptions>();
        services.AddHttpClient<IWashAiHttpClientService, WashAiHttpClientService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(washAiOptions.ApiUrl);
            httpClient.Timeout = Timeout.InfiniteTimeSpan;
        })
       .ConfigurePrimaryHttpMessageHandler(() =>
       {
           return new HttpClientHandler();
       })
       .SetHandlerLifetime(Timeout.InfiniteTimeSpan);


        return services;
    }
}