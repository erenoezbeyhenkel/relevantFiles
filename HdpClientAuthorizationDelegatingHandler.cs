using Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace Hcb.Rnd.Pwn.Infrastructure.DelegatingHandlers.Authorization;

/// <summary>
/// https://www.milanjovanovic.tech/blog/extending-httpclient-with-delegating-handlers-in-aspnetcore
/// </summary>
/// <param name="tokenProvider"></param>
/// <param name="scopes"></param>
public sealed class HdpClientAuthorizationDelegatingHandler(IOptions<HdpOptions> hdpOptions) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Add Authorization header
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", hdpOptions.Value.Token);

        // Proceed with the request
        return await base.SendAsync(request, cancellationToken);
    }
}
