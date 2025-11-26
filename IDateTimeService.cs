namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.Common;

/// <summary>
/// Date time provider interface for the project. Please avoid using DateTime directly in the code. Use IDateTimeProvider instead.
/// </summary>
public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime Today { get; }
    DateTime UtcNow { get; }
    DateTimeOffset DateTimeOffset { get; }
}
