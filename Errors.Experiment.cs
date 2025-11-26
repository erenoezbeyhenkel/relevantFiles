using ErrorOr;

namespace Hcb.Rnd.Pwn.Domain.Errors;

/// <summary>
/// https://www.youtube.com/watch?v=tZ8gGqiq_IU&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=5&ab_channel=AmichaiMantinband
/// </summary>
public static partial class Errors
{
    public static class Experiment
    {
        public static Error ExperimentAlreadyExists => Error.Validation
            (
            code: "Experiment.ExperimentAlreadyExists",
            description: "Experiment Already Exists"
            );

        public static Error ExperimentCreationError => Error.Failure
            (
            code: "Experiment.ExperimentCreationError",
            description: "Experiment Creation Error"
            );

        public static Error NoExperimentExists => Error.Validation
            (
            code: "Experiment.NoExperimentExists",
            description: "No Experiment Exists"
            );

        public static Error ExperimentDoesNotExist => Error.Failure
            (
            code: "Experiment.ExperimentDoesNotExist",
            description: "Experiment Does Not Exist"
            );

        public static Error ExperimentDeleteError => Error.Failure
            (
            code: "Experiment.ExperimentDeleteError",
            description: "Experiment Delete Error"
            );


        public static Error InvalidStatusChange(string name) => Error.Validation
         (
         code: "Experiment.ExperimentStatusChangeNotAllowed",
         description: "Experiment Status Change not allowed"
         );
    }
}
