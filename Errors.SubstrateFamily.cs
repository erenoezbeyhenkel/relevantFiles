using ErrorOr;

namespace Hcb.Rnd.Pwn.Domain.Errors;

/// <summary>
/// https://www.youtube.com/watch?v=tZ8gGqiq_IU&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=5&ab_channel=AmichaiMantinband
/// </summary>
public static partial class Errors
{
    public static class SubstrateFamily
    { 
        public static Error SubstratesAreUsed => Error.Validation
            (
            code: "SubstrateFamily.SubstratesAreUsed",
            description: "The substrate family contains the substrates used by the experiment. You cannot delete the substrate family."
            );
    }
}
