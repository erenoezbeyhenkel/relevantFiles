using Hcb.Rnd.Pwn.Common.Extensions;
using Newtonsoft.Json;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions;

/// <summary>
/// Created by EO.
/// </summary>
public static class JsonExtensions
{
    public static string ToJson(this object o)
    {
        if (o is string oString)
            return oString;

        if (Guard.Against.IsNull(o))
            return string.Empty;

        try
        {
            return JsonConvert.SerializeObject(o);
        }
        catch
        {
            try
            {
                return System.Text.Json.JsonSerializer.Serialize(o);
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    public static T FromJson<T>(this string s, T defaultT = default)
    {
        if (Guard.Against.IsNullOrEmpty(s))
            return defaultT;

        try
        {
            return JsonConvert.DeserializeObject<T>(s);
        }
        catch
        {
            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<T>(s);
            }
            catch
            {
                try
                {
                    return (T)Convert.ChangeType(s, typeof(T));
                }
                catch
                {
                    return defaultT;
                }
            }
        }
    }
}
