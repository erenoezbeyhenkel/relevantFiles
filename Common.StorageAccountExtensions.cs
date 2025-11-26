using System.Web;

namespace Hcb.Rnd.Pwn.Common.Extensions;

public static class StorageAccountExtensions
{
    public static bool IsTokenValid(this Uri uri, DateTime now)
    {
        if (Guard.Against.IsNull(uri))
            return false;

        var sasTokenQueryParams = HttpUtility.ParseQueryString(uri.Query);
        if (Guard.Against.IsNull(sasTokenQueryParams))
            return false;

        var seDateTimeSignature = sasTokenQueryParams["se"];
        if(Guard.Against.IsNull(seDateTimeSignature))
            return false;

        var expiresOn = DateTime.Parse(seDateTimeSignature, null, System.Globalization.DateTimeStyles.RoundtripKind);

        //If token is not expired
        if (now < expiresOn)
            return true;

        return false;
    }
}
