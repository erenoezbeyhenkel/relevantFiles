using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Create;
using Hcb.Rnd.Pwn.Common.Extensions;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Orders.Create;

public sealed class CreateOrderCommandValidator : BaseValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(c => c.Order.Description)
            .NotNull();

        RuleFor(c => c.Order.HugoProjectId)
            .NotNull();

        RuleFor(c => c.Order.ProductDeveloperAadGroup.Id)
            .NotNull()
            .GreaterThan(0);

        RuleFor(c => c.Order.ExperimentTemplateId)
            .NotNull()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Order.Products)
            .Must(x => Guard.Against.IsAnyOrNotEmpty(x))
            .WithMessage("At least one Product must be provided.");
    }

}
