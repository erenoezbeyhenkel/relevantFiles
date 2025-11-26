using Hcb.Rnd.Pwn.Domain.Entities.Base;
using Hcb.Rnd.Pwn.Domain.Entities.Measurements;
using Hcb.Rnd.Pwn.Domain.Entities.Products;

namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments;

public class ProductSetup : BaseEntity
{
    public long ExperimentId { get; set; }
    public Experiment Experiment { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public long? WashingMachineId { get; set; }
    public WashingMachine WashingMachine { get; set; }
    public int Position { get; set; }
    public decimal? DosageMl { get; set; }
    public decimal? Density { get; set; }
    public decimal DosageGram { get; set; }
    public decimal WaterHardness { get; set; }
    public decimal WaterLevel { get; set; }
    public decimal ChlorLevel { get; set; }
    public string MixingRatio { get; set; }
    public bool IsDiscardedForAi { get; set; }
    public bool IsDiscardedForReporting { get; set; }
    public int Pieces { get; set; }
    public int? RotationTime { get; set; }
    public int? Drumspeed { get; set; }
    public string ProcessInstructions { get; set; }
    public ICollection<FrameMonitor> FrameMonitors { get; set; } = [];
    public ICollection<ProductSetupAdditive> Additives { get; set; } = [];

}
