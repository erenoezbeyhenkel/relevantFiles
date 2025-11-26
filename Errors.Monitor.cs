using ErrorOr;

namespace Hcb.Rnd.Pwn.Domain.Errors;

/// <summary>
/// https://www.youtube.com/watch?v=tZ8gGqiq_IU&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=5&ab_channel=AmichaiMantinband
/// </summary>
public static partial class Errors
{
    public static class Monitor
    {
        public static Error MonitorAlreadyExists => Error.Validation
            (
            code: "Monitor.MonitorAlreadyExists",
            description: "Monitor Already Exists"
            );

        public static Error MonitorCreationError => Error.Failure
           (
           code: "Monitor.MonitorCreationError",
           description: "Monitor Creation Error"
           );

        public static Error NoMonitorExists => Error.Validation
            (
            code: "Monitor.MonitorCreationError",
            description: "No Monitor Exists"
            );

        public static Error MonitorDoesNotExist => Error.Failure
           (
           code: "Monitor.MonitorDoesNotExist",
           description: "Monitor Does Not Exist"
           );

        public static Error MonitorDeleteError => Error.Failure
           (
           code: "Monitor.MonitorDeleteError",
           description: "Monitor Delete Error"
           );

        public static Error AlreadyInUse(long experimentId) => Error.Validation
            (
            code: $"Monitor.AlreadyInUse",
            description: $"Monitor is already in use for Experiment Id : {experimentId}."
            );

    }
}
