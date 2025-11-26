using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.StorageAccount.DeleteBlobs;
using Hcb.Rnd.Pwn.Common.Extensions;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.StorageAccount.DeleteBlobs;

public sealed class DeleteBlobsCommandValidator : BaseValidator<DeleteBlobsCommand>
{
    public DeleteBlobsCommandValidator()
    {
        RuleFor(c => c.DeleteBlobDtos)
           .Must(c => Guard.Against.IsAnyOrNotEmpty(c))
           .WithMessage("At least one blob data must be provided.")
           .ChildRules(c =>
               c.RuleForEach(member => member)
               .SetValidator(new DeleteBlobDtoValidator())
           );
    }
}