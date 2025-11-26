using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Expand;
using Hcb.Rnd.Pwn.Common.Extensions;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Orders.Expand;

public sealed class ExpandOrderCommandValidator : BaseValidator<ExpandOrderCommand>
{
    public ExpandOrderCommandValidator()
    {
        RuleFor(c => c.ExpandOrderDto.Order.Description)
            .NotNull();

        RuleFor(c => c.ExpandOrderDto.Order.HugoProjectId)
            .NotNull();

        RuleFor(c => c.ExpandOrderDto.Order.ProductDeveloperAadGroup.Id)
            .NotNull()
            .GreaterThan(0);

        RuleFor(c => c.ExpandOrderDto.Order.ExperimentTemplateId)
            .NotNull()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ExpandOrderDto.Order.Products)
            .Must(x => Guard.Against.IsAnyOrNotEmpty(x))
            .WithMessage("At least one Product must be provided.");
    }
}
