using ErrorOr;

namespace Hcb.Rnd.Pwn.Domain.Errors;

/// <summary>
/// https://www.youtube.com/watch?v=tZ8gGqiq_IU&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=5&ab_channel=AmichaiMantinband
/// </summary>
public static partial class Errors
{
    public static class Infrastructure
    {
        public static Error InsertError => Error.Failure
            (
            code: "Infrastructure.InsertError",
            description: "Erros occured while InsertOneAsync() method runs into GenericRepository"
            );

        public static Error InfrastructureErrorStaticDescription => Error.Failure
            (
            code: "Infrastructure.InfrastructureError",
            description: "Error occured during the process."
            );

        public static Error InfrastructureError(string errorMessage) => Error.Failure
            (
            code: "Infrastructure.InfrastructureError",
            description: errorMessage
            );

        public static Error InfrastructureError(string source, string errorMessage, string stackTrace) => Error.Custom
            (
            type: 1000,
            code: "Infrastructure.InfrastructureError",
            description: $"SOURCE: {source}//ERRORMESSAGE: {errorMessage}// STACKTRACE: {stackTrace}"
            );

        public static Error AlreadyExists(string name) => Error.Validation
           (
           code: $"Infrastructure.{name}AlreadyExists",
           description: $"{name} Already Exists"
           );

        public static Error DoesNotExist(string name) => Error.Validation
          (
          code: $"Infrastructure.{name}DoesNotExist",
          description: $"{name} Does Not Exist"
          );

        public static Error CreationError(string name) => Error.Failure
           (
           code: $"Infrastructure.{name}CreationError",
           description: $"{name} Creation Error"
           );

        public static Error DeleteError(string name) => Error.Failure
          (
          code: $"Infrastructure.{name}DeleteError",
          description: $"{name} Delete Error"
          );

        public static Error UpdateError(string name) => Error.Failure
         (
         code: $"Infrastructure.{name}UpdateError",
         description: $"{name} Update Error"
         );

        public static Error AlreadyInUse(string name) => Error.Validation
            (
            code: $"Infrastructure.{name}AlreadyInUse",
            description: $"{name} is already in use."
            );

        public static Error AlreadyInUseAt(string message) => Error.Validation
          (
          code: $"Infrastructure.AlreadyInUse",
          description: message
          );

        public static Error InvalidPercentage(string name) => Error.Validation
           (
           code: $"Infrastructure.{name}InvalidPercentage",
           description: $"The total percentage of {name} must be %100."
           );

        public static Error InvalidFile => Error.Validation
         (
         code: $"Infrastructure.InvalidFile",
         description: $"Selected files mach5 file numbers don't match. All files must belong to same mach5 file number."
         );

        public static Error MeasuredDataSubAreasDoesNotExist => Error.Validation
       (
       code: $"Infrastructure.MeasuredDataSubAreasDoesNotExist",
       description: $"Selected files don't contain Measured Data Sub Areas file which is required to read the measured color metric values."
       );

        public static Error InvalidColorMetricData(string message) => Error.Validation
          (
          code: $"Infrastructure.InvalidColorMetricData",
          description: message
          );

        public static Error ProductsCountDontMatch(string message) => Error.Validation
         (
         code: $"Infrastructure.ProductsCountDontMatch",
         description: message
         );

        public static Error IncorrectGanzValue(string message) => Error.Validation
        (
            code: $"Infrastructure.IncorrectGanzValue",
            description: message
        );
    }
}
