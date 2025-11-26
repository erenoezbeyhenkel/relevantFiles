using ErrorOr;

namespace Hcb.Rnd.Pwn.Domain.Errors;

/// <summary>
/// https://www.youtube.com/watch?v=tZ8gGqiq_IU&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=5&ab_channel=AmichaiMantinband
/// </summary>
public static partial class Errors
{
    public static class AppConfiguration
    {
        public static Error ValueDoesNotExist(string key) => Error.Failure
            (
            code: "AppConfiguration.ValueDoesNotExist",
            description: $"There is no value in the Azure App Configuration for KEY: {key} and ENVIRONMENT : {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Process)}"
            );
    }
}
