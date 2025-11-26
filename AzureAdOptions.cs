namespace Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;

/// <summary>
/// AzureAd properties. Same property names in the appsettings.json.
/// </summary>
public sealed class AzureAdOptions
{
    public string Instance { get; set; }
    public string Domain { get; set; }
    public string TenantId { get; set; }
    public string ClientId { get; set; }
    public string CallbackPath { get; set; }
    public string ClientSecret { get; set; }
    public string[] ClientCertificates { get; set; }
    public string AuthorizationUrl { get; set; }
    public string TokenUrl { get; set; }
    public string[] Scopes { get; set; }
    public string KeyVaultUrl { get; set; }
}
