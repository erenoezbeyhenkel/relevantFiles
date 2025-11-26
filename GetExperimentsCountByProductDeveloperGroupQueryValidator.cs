using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment.GetExperimentsByProductDeveloperGroup;

namespace Hcb.Rnd.Pwn.Application.Features.Queries.Experiments.Experiment.GetExperimentsByProductDeveloperGroup;

public sealed class GetExperimentsCountByProductDeveloperGroupQueryValidator:BaseValidator<GetExperimentsCountByProductDeveloperGroupQuery>
{
    public GetExperimentsCountByProductDeveloperGroupQueryValidator()
    {
        RuleFor(q => q.NumberOfDays).
            NotNull().
            GreaterThan(0);

        RuleFor(q=>q.ProductDeveloperGroupId).
            NotNull();
    }
}
