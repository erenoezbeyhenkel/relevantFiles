using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.StorageAccount;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.StorageAccount.UploadBlobs;

public sealed class BlobDataDtoValidator : BaseValidator<BlobDataDto>
{
    public BlobDataDtoValidator()
    {
        RuleFor(x => x.ContainerName)
            .NotNull();

        RuleFor(x => x.BlobName)
            .NotNull();

        RuleFor(x => x.BlobData)
            .NotNull();
    }
}

