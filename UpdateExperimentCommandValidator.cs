using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Update;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Experiments.Experiment.Update;

public sealed class UpdateExperimentCommandValidator : BaseValidator<UpdateExperimentCommand>
{
    public UpdateExperimentCommandValidator()
    {
        RuleFor(c => c.UpdateExperimentDto.Id)
            .NotNull()
            .GreaterThan(0);

        RuleFor(c => c.UpdateExperimentDto.ExperimentTypeId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(c => c.UpdateExperimentDto.ExperimentStatusId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(c => c.UpdateExperimentDto.NumberOfRepetition)
            .NotNull()
            .GreaterThan(0);
    }
}
