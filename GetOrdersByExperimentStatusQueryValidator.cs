using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrdersByExperimentStatus;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Orders.GetOrdersByExperimentStatus;

public sealed class GetOrdersByExperimentStatusQueryValidator :BaseValidator<GetOrdersByExperimentStatusQuery>
{
    public GetOrdersByExperimentStatusQueryValidator()
    {
        RuleFor(q => q.Cursor)
            .NotNull()
            .GreaterThan(0);

        RuleFor(q => q.From)
            .NotNull();

        RuleFor(q => q.To)
            .NotNull();

        RuleFor(q => q.ExperimentStatus)
            .NotNull();
    }
}
