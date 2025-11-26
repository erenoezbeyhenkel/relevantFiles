using Hcb.Rnd.Pwn.Domain.Entities.Base;

namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments;

public sealed class TemplateSetting : BaseEntity
{
    public long ExperimentTemplateId { get; set; }
    public Experiment ExperimentTemplate { get; set; }
    public decimal WaterHardness { get; set; }
    public decimal WaterLevel { get; set; }
    public decimal ChlorLevel { get; set; }
    public string MixingRatio { get; set; }
    public int? RotationTime { get; set; }
    public int? Drumspeed { get; set; }
}
