using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetPillingImagesById;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Experiments.Experiment.GetPillingImagesById;

public sealed class GetPillingImagesByExperimentIdQueryValidator : BaseValidator<GetPillingImagesByExperimentIdQuery>
{
    public GetPillingImagesByExperimentIdQueryValidator()
    {
        RuleFor(q => q.ExperimentId)
            .NotNull()
            .GreaterThan(1);
    }
}
