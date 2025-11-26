using Hcb.Rnd.Pwn.Application.Common.Constants;
using System.Security.Claims;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal? principal) => principal?.FindFirstValue(GeneralConstants.ObjectIdentifier) ?? throw new ApplicationException("User id is unavailable");
    public static string GetUserEmail(this ClaimsPrincipal? principal) => principal?.FindFirstValue(GeneralConstants.PreferredUsername) ?? throw new ApplicationException("User email is unavailable");
}