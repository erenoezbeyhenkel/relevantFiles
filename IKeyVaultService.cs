using System.Security.Cryptography.X509Certificates;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.KeyVault;

public interface IKeyVaultService
{
    Task<X509Certificate2> GetCertificateAsync(string certificateName, CancellationToken cancellationToken, string password = null);
    Task<X509Certificate2> DownloadCertificateAsync(string certificateName, CancellationToken cancellationToken);
}
