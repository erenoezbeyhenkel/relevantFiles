using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Create;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Experiments.Experiment.Create;

public sealed class CreateExperimentCommandValidator : BaseValidator<CreateExperimentCommand>
{
    public CreateExperimentCommandValidator()
    {
        RuleFor(c => c.CreateExperimentDto.ExperimentTemplateId)
          .NotNull()
          .GreaterThan(0);

        RuleFor(c => c.CreateExperimentDto.OrderId)
            .NotNull()
          .GreaterThan(0);
    }
}
