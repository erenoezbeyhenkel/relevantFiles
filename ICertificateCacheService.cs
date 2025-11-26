using System.Security.Cryptography.X509Certificates;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.KeyVault;


public interface ICertificateCacheService
{
    Task<X509Certificate2> GetCertificateAsync(string certificateName, CancellationToken cancellationToken);
}