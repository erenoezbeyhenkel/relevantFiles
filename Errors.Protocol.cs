using ErrorOr;

namespace Hcb.Rnd.Pwn.Domain.Errors;

/// <summary>
/// https://www.youtube.com/watch?v=tZ8gGqiq_IU&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=5&ab_channel=AmichaiMantinband
/// </summary>
public static partial class Errors
{
    public static class Protocol
    {
        public static Error ProtocolIsUsed => Error.Validation
            (
            code: "Protocol.ProtocolIsUsed",
            description: "This protocol assigned to any of the substrate. You cannot delete the protocol."
            );

        public static Error ProtocolIsUsedCanNotBeInActive => Error.Validation
            (
            code: "Protocol.ProtocolIsUsed",
            description: "This protocol assigned to any of the substrate. You cannot make it InActive."
            );
    }
}
