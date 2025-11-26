using FluentValidation;
using Hcb.Rnd.Pwn.Application.Common.Validator;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.ChangeStatus;

namespace Hcb.Rnd.Pwn.Application.Features.Commands.Experiments.Experiment.ChangeStatus;

public sealed class ChangeStatusExperimentCommandValidator : BaseValidator<ChangeStatusExperimentCommand>
{
    public ChangeStatusExperimentCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .GreaterThan(0);

        RuleFor(c => c.OrderId)
         .NotNull()
         .GreaterThan(0);

        RuleFor(c => c.ExperimentStatusId)
            .NotNull()
            .GreaterThan(0);    
    }
}
