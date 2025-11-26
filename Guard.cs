namespace Hcb.Rnd.Pwn.Common.Extensions;

/// <summary>
/// Added by EO. No direct source code.
/// To avoid individuals null check or any other checks in the code. Use Guard Clauses instead.
/// </summary>
public interface IGuardClause { }
public class Guard : IGuardClause
{
    public static IGuardClause Against { get; } = new Guard();
    public static IGuardClause Control { get; } = new Guard();
    private Guard() { }
}

public static class GuardClauseExtensions
{
    public static void Null(this IGuardClause guardClause, object input, string parameterName)
    {
        ArgumentNullException.ThrowIfNull(input, parameterName);
    }

    public static bool IsNull(this IGuardClause guardClause, object input)
    {
        return input == null;
    }
    public static void NullOrEmpty(this IGuardClause guardClause, string input, string parameterName)
    {
        ArgumentException.ThrowIfNullOrEmpty(input, parameterName);
    }
    public static bool IsNullOrEmpty(this IGuardClause guardClause, string input)
    {
        bool isNull = Guard.Against.IsNull(input);
        return isNull || input == string.Empty || string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input);

    }
    public static void NullOrEmpty<T>(this IGuardClause guardClause, IEnumerable<T> input, string parameterName)
    {
        Guard.Against.Null(input, parameterName);
        if (!input.Any())
        {
            throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
        }
    }
    public static void NullOrWhiteSpace(this IGuardClause guardClause, string input, string parameterName)
    {
        Guard.Against.NullOrEmpty(input, parameterName);
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
        }
    }
    public static void Zero<T>(this IGuardClause guardClause, T input, string parameterName)
    {
        if (EqualityComparer<T>.Default.Equals(input, default))
        {
            throw new ArgumentException($"Required input {parameterName} cannot be zero.", parameterName);
        }
    }

    public static bool IsZero<T>(this IGuardClause guardClause, T input)
    {
        if (EqualityComparer<T>.Default.Equals(input, default))
            return true;

        return false;
    }

    public static bool IsInvalidDouble(this IGuardClause guardClause, double input)
    {
        return double.IsNaN(input) || double.IsInfinity(input);
    }
    public static bool IsAnyOrNotEmpty<T>(this IGuardClause guardClause, IEnumerable<T> collection)
    {
        if (Guard.Against.IsNull(collection))
            return false;

        if (collection.Any())
            return true;

        foreach (var item in collection)
        {
            if (item is not string itemStr)
                continue;

            return Guard.Against.IsNullOrEmpty(itemStr);
        }
        return false;
    }

    public static bool IsEqual(this IGuardClause guardClause, string value1, string value2) => string.Equals(value1, value2, StringComparison.Ordinal);
    public static bool IsEqual(this IGuardClause guardClause, char value1, char value2) => Equals(value1, value2);
}
