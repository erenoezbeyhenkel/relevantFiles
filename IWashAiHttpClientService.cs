using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.WashAi.Evaluation.Delete;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.WashAi.Evaluation.ExecuteStatisticsByWashCycleSetup;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.HttpClients;

public interface IWashAiHttpClientService
{
    Task<DeleteEvaluationsByWashCycleSetupsCommandResponse> DeleteEvaluationsByWashCycleSetupsAsync(DeleteEvaluationsByWashCycleSetupsCommand deleteEvaluationsByWashCycleSetupsCommand, CancellationToken cancellationToken);
    Task<ExecuteStatisticsByWashCycleSetupCommandResponse> ExecuteStatisticsByWashCycleSetupAsync(ExecuteStatisticsByWashCycleSetupCommand executeStatisticsByWashCycleSetupCommand, CancellationToken cancellationToken);
}
