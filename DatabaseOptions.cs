namespace Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;

public sealed class DatabaseOptions
{
    public string OnPremSqlServerConnectionString { get; set; }
    public string AzureSqlServerConnectionString { get; set; }
    public int MaxRetryCount { get; set; }
    public int CommandTimeOut { get; set; }
    public bool EnabledDetailedError { get; set; }
    public bool EnableSensitiveDataLogging { get; set; }
}