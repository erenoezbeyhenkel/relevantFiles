using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Update;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Orders.Update;

public sealed class UpdateOrderCommandValidator : BaseValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Order.Id)
            .NotNull();

        RuleFor(c => c.Order.InternalId)
        .NotNull();

        RuleFor(c => c.Order.Description)
            .NotNull();

        RuleFor(c => c.Order.HugoProjectId)
            .NotNull();
    }
}
