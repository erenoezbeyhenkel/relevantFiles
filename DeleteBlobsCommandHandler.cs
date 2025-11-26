using ErrorOr;
using Hcb.Rnd.Pwn.Application.Interfaces.Messaging;
using Hcb.Rnd.Pwn.Application.Interfaces.Services.StorageAccount;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.StorageAccount.DeleteBlobs;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.StorageAccount.DeleteBlobs;

public sealed class DeleteBlobsCommandHandler(IStorageAccountService storageAccountService) : ICommandHandler<DeleteBlobsCommand, DeleteBlobsCommandResponse>
{
    public async Task<ErrorOr<DeleteBlobsCommandResponse>> Handle(DeleteBlobsCommand request, CancellationToken cancellationToken)
    {
        await storageAccountService.DeleteBlobsAsync(request.DeleteBlobDtos, cancellationToken);
        return new DeleteBlobsCommandResponse();
    }
}
