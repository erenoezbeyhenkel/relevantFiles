namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.HttpClients;

public interface ILabvantageAuthenticationHttpClientService
{
    Task<string> GetLabvantageConnectionIdAsync();
    Task DeleteLabvantageConnectinId();
    Task<bool> ValidateLabvantageConnectionId();
}
