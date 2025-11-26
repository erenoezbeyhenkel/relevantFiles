using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Base;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.Experiment.Update;

public sealed record UpdateExperimentCommand(UpdateExperimentDto UpdateExperimentDto) : BaseCommand<UpdateExperimentCommandResponse>;
