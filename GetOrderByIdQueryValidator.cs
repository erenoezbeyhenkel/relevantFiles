using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrderById;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Orders.GetOrderById;

public sealed class GetOrderByIdQueryValidator : BaseValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(q => q.OrderId)
            .NotNull()
            .GreaterThan(0);
    }
}
