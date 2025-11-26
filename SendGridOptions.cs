namespace Hcb.Rnd.Pwn.Application.Common.AppConfigurationOptions;

/// <summary>
/// Sendgrid configuration values.
/// </summary>
public sealed class SendGridOptions
{
    public string FromEmailId { get; set; }
    public string FromEmailName { get; set; }
    public string AssignmentCompleteTemplateId { get; set; }
    public string WashingMachineMismatchTemplateId { get; set; }
    public string TemplateExperimentCreationTemplateId { get; set; }
    public string ExperimentStatusChangingOpenToValidationTemplateId { get; set; }
    public string ExperimentStatusChangingMeasurementToCompleteTemplateId { get; set; }
    public string ExperimentStatusChangingValidationToPreparationTemplateId { get; set; }
    public string SynchronizeWashingMachinesJobTemplateId { get; set; }
    public string ExperimentLoadingWashRunNotificationTemplateId { get; set; }
    public bool OnlyDevTeam { get; set; }

}
