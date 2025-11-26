using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetById;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Experiments.Experiment.GetById;

public sealed class GetExperimentByIdQueryValidator : BaseValidator<GetExperimentByIdQuery>
{
    public GetExperimentByIdQueryValidator()
    {
        RuleFor(c => c.ExperimentId)
            .NotNull()
            .GreaterThan(0);
    }
}
