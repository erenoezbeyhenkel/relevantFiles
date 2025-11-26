namespace Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;

public sealed class StorageAccountOptions
{
    public string Name { get; set; }
    public string Key { get; set; }
    public string ServiceUrl { get; set; }
    public int ExpiresInHours { get; set; }
}
