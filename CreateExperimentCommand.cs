using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Base;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Create;

public sealed record CreateExperimentCommand(CreateExperimentDto CreateExperimentDto) : BaseCommand<CreateExperimentCommandResponse>;



