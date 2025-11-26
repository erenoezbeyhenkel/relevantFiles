using Microsoft.AspNetCore.Http;
using Hcb.Rnd.Pwn.Application.Interfaces.Authentication;
using System.Security.Claims;
using System.Security.Principal;
using Hcb.Rnd.Pwn.Application.Common.Extensions;
using Hcb.Rnd.Pwn.Application.Common.Constants;
using Microsoft.Extensions.Caching.Hybrid;

namespace Hcb.Rnd.Pwn.Infrastructure.Authentication;

/// <summary>
/// Use this once you need any information from IHttpContextAccessor. To centralized the values and avoid code repeats.
/// First define the property in IPwnHttpContextAccessor then implement here.
/// </summary>
/// <param name="httpContextAccessor"></param>
public sealed class PwnHttpContextAccessor(IHttpContextAccessor httpContextAccessor,
                                           HybridCache hybridCache) : IPwnHttpContextAccessor
{
    public ClaimsPrincipal User => httpContextAccessor.HttpContext?.User;
    public IIdentity Identity => User?.Identity;
    public bool? IsAuthenticated => Identity?.IsAuthenticated;
    public string UserEmail => IsAuthenticated is true
        ? GetUserEmail()
        : "AnonymousEmail";
    public string UserId => IsAuthenticated is true
        ? User.GetUserId()
        : GetUnauthanticatedUserId().Result;
    public HttpContext HttpContext => httpContextAccessor.HttpContext;

    public List<string> UserGroups => User.Claims?.Where(x => x.Type == GeneralConstants.Groups)?.Select(c => c.Value)?.ToList();

    private async Task<string> GetUnauthanticatedUserId()
    {
        var userId = await hybridCache.GetOrCreateAsync(
           HybridCacheConstants.UserId,
           async _ => await GetUserIdAsync(),
           cancellationToken: default,
           options: default
       );

        return userId;
    }

    //If there is no user Id in the cache then the user Id will be AnonymousUser.
    private static async Task<string> GetUserIdAsync()
    {
        await Task.CompletedTask;
        return "AnonymousUser";
    }

    string GetUserEmail()
    {
        try
        {
            return User.GetUserId();
        }
        catch
        {
            return User.Claims?.FirstOrDefault(x => x.Type == GeneralConstants.Oid).Value;
        }
    }

}