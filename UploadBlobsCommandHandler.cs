using ErrorOr;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.StorageAccount;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.StorageAccount.UploadBlobs;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.StorageAccount.UploadBlobs;

public sealed class UploadBlobsCommandHandler(IStorageAccountService storageAccountService) : ICommandHandler<UploadBlobsCommand, UploadBlobsCommandResponse>
{
    public async Task<ErrorOr<UploadBlobsCommandResponse>> Handle(UploadBlobsCommand request, CancellationToken cancellationToken)
    {
        await storageAccountService.UploadBlobsAsync(request.BlobDataDtos, cancellationToken);
        return new UploadBlobsCommandResponse();
    }
}
