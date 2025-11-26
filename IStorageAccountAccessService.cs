namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.StorageAccount;

public interface IStorageAccountAccessService
{
    Task<string> CreateAccountSasToken();
}
