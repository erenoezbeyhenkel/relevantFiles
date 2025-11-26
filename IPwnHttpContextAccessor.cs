using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Authentication;

/// <summary>
/// Use this, once you need any information from IHttpContextAccessor. To centralized the values and avoid code repeats.
/// First, define the property in here then implement them in PwnHttpContextAccessor.
/// </summary>
public interface IPwnHttpContextAccessor
{
    string UserId { get; }
    string UserEmail { get; }
    bool? IsAuthenticated { get; }
    ClaimsPrincipal User { get; }
    IIdentity Identity { get; }
    HttpContext HttpContext { get; }
    List<string> UserGroups { get; }
}
