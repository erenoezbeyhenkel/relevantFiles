namespace Hcb.Rnd.Pwn.Common.Extensions;

public static class DateTimeExtensions
{
    public static long ToUnixTimestampAsLong(this DateTime dateTime) => new DateTimeOffset(dateTime.ToUniversalTime()).ToUnixTimeSeconds();
    public static long ToUnixTimestampAsLong(this DateTime? dateTime) => dateTime.HasValue ? new DateTimeOffset(dateTime.Value.ToUniversalTime()).ToUnixTimeSeconds() : default;
    public static string ToUnixTimestampAsString(this DateTime dateTime) => new DateTimeOffset(dateTime.ToUniversalTime()).ToUnixTimeSeconds().ToString();
    public static string ToUnixTimestampAsString(this DateTime? dateTime) => dateTime.HasValue ? new DateTimeOffset(dateTime.Value.ToUniversalTime()).ToUnixTimeSeconds().ToString() : default;
}
