using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Hcb.Rnd.Pwn.Common.Extensions;

public static class GeneralExtensions
{
    public static int ExtractNumber(this string fileName)
    {
        var regex = new Regex(@"\d+");
        var match = regex.Match(fileName);

        return match.Success ? Convert.ToInt32(match.Value) : 0;
    }

    public static string RemoveNonLetterOrDigit(this string input)
    {
        if (Guard.Against.IsNullOrEmpty(input))
            return input;

        StringBuilder result = new();
        foreach (char c in input)
        {
            if (char.IsLetterOrDigit(c))
                result.Append(c);
        }

        return result.ToString();
    }

    public static decimal ToDecimal(this string value, decimal defaultValue)
    {
        var formatinfo = new NumberFormatInfo
        {
            NumberDecimalSeparator = "."
        };

        if (decimal.TryParse(value, NumberStyles.Float, formatinfo, out decimal d))
            return d;

        formatinfo.NumberDecimalSeparator = ",";

        if (decimal.TryParse(value, NumberStyles.Float, formatinfo, out d))
            return d;

        return defaultValue;
    }

    public static string ToDictionaryString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return "{" + string.Join(",", [.. dictionary.Select(kv => kv.Key + "=" + kv.Value)]) + "}";
    }

    public static async Task<string> HttpRequestMessageToString(this HttpRequestMessage request)
    {
        var requestString = new StringBuilder();
        requestString.AppendLine($"Request Method: {request.Method} ");
        requestString.AppendLine($"Request URI:  {request.RequestUri} ");

        if (request.Content != null)
        {
            requestString.AppendLine("Request Body: ");
            requestString.AppendLine(await request.Content.ReadAsStringAsync());
        }

        return requestString.ToString();
    }

    public static async Task<string> HttpResponseMessageToString(this HttpResponseMessage response)
    {
        var requestString = new StringBuilder();
        requestString.AppendLine($"Status Code: {(int)response.StatusCode} - {response.StatusCode} ");

        if (response.Content != null)
        {
            requestString.AppendLine("Response Body: ");
            requestString.AppendLine(await response.Content.ReadAsStringAsync());
        }

        return requestString.ToString();
    }

    public static double ToDouble(this string value)
    {
        var formatinfo = new NumberFormatInfo
        {
            NumberDecimalSeparator = "."
        };

        if (double.TryParse(value, NumberStyles.Float, formatinfo, out double d))
            return d;

        formatinfo.NumberDecimalSeparator = ",";

        if (double.TryParse(value, NumberStyles.Float, formatinfo, out d))
            return d;

        return 0.0;
    }
}
