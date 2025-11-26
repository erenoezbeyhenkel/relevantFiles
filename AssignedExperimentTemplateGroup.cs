using Hcb.Rnd.Pwn.Domain.Entities.AzureGroups;
using Hcb.Rnd.Pwn.Domain.Entities.Base;

namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments;

public sealed class AssignedExperimentTemplateGroup : BaseEntity
{
    public long ExperimentId { get; set; }
    public Experiment Experiment { get; set; }
    public long AssignedExperimentProductDeveloperAadGroupId { get; set; }
    public ProductDeveloperAadGroup ProductDeveloperAadGroup { get; set; }
}
