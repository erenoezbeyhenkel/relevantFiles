using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.StorageAccount.DeleteBlobs;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.StorageAccount.DeleteBlobs;

public sealed class DeleteBlobDtoValidator : BaseValidator<DeleteBlobDto>
{
    public DeleteBlobDtoValidator()
    {
        RuleFor(x => x.ContainerName)
            .NotNull();

        RuleFor(x => x.BlobName)
            .NotNull();
    }
}