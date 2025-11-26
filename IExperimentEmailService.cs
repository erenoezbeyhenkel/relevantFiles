using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.MicrosoftGraph;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.Email.Experiment;

public interface IExperimentEmailService
{
    Task SendExperimentStatusChangingOpenToValidationEmailAsync(string validatorAadGroupId, string validatorAadGroupName, Domain.Entities.Experiments.Experiment experiment, CancellationToken cancellationToken);
    Task SendExperimentStatusChangingValidationToPreparationTemplateEmailAsync(string operatorAadGroupId, string operatorAadGroupName, string productDeveloperAadGroupId, Domain.Entities.Experiments.Experiment experiment, CancellationToken cancellationToken);
    Task SendExperimentStatusChangingMeasurementToCompleteTemplateEmailAsync(string validatorAadGroupId, string productDeveloperAadGroupId, Domain.Entities.Experiments.Experiment experiment, CancellationToken cancellationToken);
    Task SendTemplateExperimentChangesEmailAsync(List<MemberNameAndEmailDto> tos, Domain.Entities.Experiments.Experiment experimentTemplate, CancellationToken cancellationToken);
    Task SendExperimentLoadingWashRunNotificationEmail(long experimentId, string excelBase64);
}
