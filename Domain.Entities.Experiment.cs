using Hcb.Rnd.Pwn.Domain.Entities.AzureGroups;
using Hcb.Rnd.Pwn.Domain.Entities.Base;
using Hcb.Rnd.Pwn.Domain.Entities.Dashboard;
using Hcb.Rnd.Pwn.Domain.Entities.Orders;

namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments;

public class Experiment : BaseEntity
{
    public long? OrderId { get; set; }
    public Order Order { get; set; }
    public long ExperimentTypeId { get; set; }
    public ExperimentType ExperimentType { get; set; }
    public long ExperimentStatusId { get; set; }
    public ExperimentStatus ExperimentStatus { get; set; }
    public long? ValidatorAadGroupId { get; set; }
    public ValidatorAadGroup ValidatorAadGroup { get; set; }
    public long? OperatorAadGroupId { get; set; }
    public OperatorAadGroup OperatorAadGroup { get; set; }
    public TemplateSetting TemplateSetting { get; set; }
    public DateTime StatusOpenDate { get; set; }
    public DateTime? StatusValidationDate { get; set; }
    public DateTime? StatusPreparationDate { get; set; }
    public DateTime? StatusMeasurementDate { get; set; }
    public DateTime? StatusCompletedDate { get; set; }
    public DateTime? ProductDeliveryDate { get; set; }
    public DateTime? StartingCalendarWeekDate { get; set; }

    public string Description { get; set; }
    public string Comment { get; set; }
    public int NumberOfRepetition { get; set; }
    public bool IsActive { get; set; }
    public long? TemplateExperimentId { get; set; }
    public string RequestedProgram { get; set; }
    public int RequestedTemperature { get; set; }
    public bool IsExternal { get; set; }

    public ICollection<MonitorSetup> MonitorSetups { get; set; } = [];
    public ICollection<ProductSetup> ProductSetups { get; set; } = [];
    public ICollection<TemplateAdditive> TemplateAdditives { get; set; } = [];
    public ICollection<AssignedExperimentTemplateGroup> AssignedExperimentTemplateGroups { get; set; } = [];
    public ICollection<RecentlyViewedExperiment> RecentlyViewedExperiments { get; set; } = [];
    public ICollection<RecentlyViewedExperimentTemplate> RecentlyViewedExperimentTemplates { get; set; } = [];

}
