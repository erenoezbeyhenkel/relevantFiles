using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.StorageAccount.DeleteBlobs;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.StorageAccount;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.StorageAccount.GetBlobsByName;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.StorageAccount;

public interface IStorageAccountService
{
    Task<BlobDataDto> GetBlobAsync(BlobByNameQueryDto blobsByNameQueryDto, CancellationToken cancellationToken);
    Task<List<BlobDataDto>> GetBlobsAsync(List<BlobByNameQueryDto> blobDataDtos, CancellationToken cancellationToken);
    Task UploadBlobsAsync(List<BlobDataDto> blobDataDtos, CancellationToken cancellationToken);
    Task DeleteBlobsAsync(List<DeleteBlobDto> deleteBlobDtos, CancellationToken cancellationToken);
}
