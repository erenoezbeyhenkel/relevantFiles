using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrdersByCursor;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Orders.GetOrdersByCursor;

public sealed class GetOrdersByCursorQueryValidator : BaseValidator<GetOrdersByCursorQuery>
{
    public GetOrdersByCursorQueryValidator()
    {
        RuleFor(q => q.Cursor)
            .NotNull()
            .GreaterThan(0);

        RuleFor(q => q.From)
            .NotNull();

        RuleFor(q => q.To)
            .NotNull();
    }
}
