using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.StorageAccount.UploadBlobs;
using Hcb.Rnd.Pwn.Common.Extensions;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.StorageAccount.UploadBlobs;

public sealed class UploadBlobsCommandValidator : BaseValidator<UploadBlobsCommand>
{
    public UploadBlobsCommandValidator()
    {
        RuleFor(c => c.BlobDataDtos)
            .Must(c => Guard.Against.IsAnyOrNotEmpty(c))
            .WithMessage("At least one blob data must be provided.")
            .ChildRules(c =>
                c.RuleForEach(member => member)
                .SetValidator(new BlobDataDtoValidator())
            );
    }
}