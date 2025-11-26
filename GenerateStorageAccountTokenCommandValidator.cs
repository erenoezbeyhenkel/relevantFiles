using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.StorageAccount.Token;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.StorageAccount.StorageAccountToken.Generate;

public sealed class GenerateStorageAccountTokenCommandValidator : BaseValidator<GenerateStorageAccountTokenCommand>
{
    public GenerateStorageAccountTokenCommandValidator()
    {
        RuleFor(c => c.GeneratedBy)
            .NotNull();
    }
}
