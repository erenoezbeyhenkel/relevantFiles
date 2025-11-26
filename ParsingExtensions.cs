using Hcb.Rnd.Pwn.Common.Extensions;
using System.Globalization;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions;

/// <summary>
/// Created by EO.
/// </summary>
public static class ParsingExtensions
{
    public static double ToDouble(this object param, double defaultValue = default)
    {
        if (Guard.Against.IsNull(param))
            return defaultValue;

        if (param is string stringParam)
        {
            NumberFormatInfo provider = new()
            {
                NumberDecimalSeparator = stringParam.Contains('.') ? "." : ",",
                NumberGroupSeparator = ","
            };

            if (!double.TryParse(stringParam, out double parsedValue))
                return defaultValue;

            double doubleVal = Convert.ToDouble(stringParam, provider);
            if (Guard.Against.IsInvalidDouble(doubleVal))
                return defaultValue;

            return doubleVal;
        }

        return defaultValue;
    }
    public static int ToInt(this object param, int defaultValue = default)
    {
        if (Guard.Against.IsNull(param))
            return defaultValue;

        if (param is string stringParam)
        {
            if (!int.TryParse(stringParam, out var parsedInt))
                return defaultValue;

            return parsedInt;
        }

        if (param is double doubleParam)
        {
            if (Guard.Against.IsInvalidDouble(doubleParam))
                return defaultValue;

            return Convert.ToInt32(doubleParam);

        }

        if (param is Int64 int64Param)
        {
            if (Guard.Against.IsInvalidDouble(int64Param))
                return defaultValue;

            return Convert.ToInt32(int64Param);

        }

        if (param is Enum enumParam)
        {
            if (Guard.Against.IsNull(enumParam))
                return defaultValue;

            return Convert.ToInt32(enumParam);
        }

        return defaultValue;
    }

    public static long ToLong(this object param, long defaultValue = default)
    {
        if (Guard.Against.IsNull(param))
            return defaultValue;

        if (param is string stringParam)
        {
            if (!long.TryParse(stringParam, out var parsedLong))
                return defaultValue;

            return parsedLong;
        }

        if (param is double doubleParam)
        {
            if (Guard.Against.IsInvalidDouble(doubleParam))
                return defaultValue;

            return Convert.ToInt64(doubleParam);

        }

        if (param is int intParam)
        {
            return Convert.ToInt64(intParam);
        }

        if (param is Enum enumParam)
        {
            if (Guard.Against.IsNull(enumParam))
                return defaultValue;

            return Convert.ToInt64(enumParam);
        }

        return defaultValue;
    }

    public static decimal ToDecimal(this object param, decimal defaultValue = default)
    {
        if (Guard.Against.IsNull(param))
            return defaultValue;

        if (param is string stringParam)
        {
            NumberFormatInfo provider = new()
            {
                NumberDecimalSeparator = stringParam.Contains('.') ? "." : ",",
                NumberGroupSeparator = ","
            };

            if (!decimal.TryParse(stringParam, out decimal parsedValue))
                return defaultValue;

            return Convert.ToDecimal(stringParam, provider);
        }

        if (param is double doubleParam)
        {
            if (Guard.Against.IsInvalidDouble(doubleParam))
                return defaultValue;

            return Convert.ToDecimal(doubleParam);

        }

        if (param is int intParam)
        {
            return Convert.ToDecimal(intParam);
        }

        if (param is Enum enumParam)
        {
            if (Guard.Against.IsNull(enumParam))
                return defaultValue;

            return Convert.ToDecimal(enumParam);
        }

        return defaultValue;
    }
}
