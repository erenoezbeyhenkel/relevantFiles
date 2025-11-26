using ErrorOr;

namespace Hcb.Rnd.Pwn.Domain.Errors;

/// <summary>
/// https://www.youtube.com/watch?v=tZ8gGqiq_IU&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=5&ab_channel=AmichaiMantinband
/// </summary>
public static partial class Errors
{
    public static class Order
    {
        public static Error CreationError => Error.Failure
            (
            code: "WashOrder.CreationError",
            description: "Erros occured while wash order creation"
            );

        public static Error WashOrderDoesNotExist => Error.Validation
            (
            code: "WashOrder.WashOrderDoesNotExist",
            description: "Wash Order Does Not Exist"
            );

        public static Error WashOrderUpdateError => Error.Failure
            (
            code: "WashOrder.WashOrderUpdateError",
            description: "Wash Order Update Error"
            );

        public static Error WashOrderDeleteError => Error.Failure
            (
            code: "WashOrder.WashOrderDeleteError",
            description: "Wash Order Delete Error"
            );

        public static Error NoWashOrderExists => Error.Validation
            (
            code: "WashOrder.NoWashOrderExists",
            description: "No Wash Order Exists"
            );
    }
}
