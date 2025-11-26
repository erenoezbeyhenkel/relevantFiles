using Hcb.Rnd.Pwn.Domain.Entities.Base;
using Hcb.Rnd.Pwn.Domain.Entities.Measurements;
using Hcb.Rnd.Pwn.Domain.Entities.Monitors;

namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments;

public class WashCycleSetup : BaseEntity
{
    public long MonitorSetupId { get; set; }
    public MonitorSetup MonitorSetup { get; set; }

    public long? MonitorBatchId { get; set; }
    public MonitorBatch MonitorBatch { get; set; }

    public bool IsInitial { get; set; }
    public int WashCycle { get; set; }
    public int? MeasurementPointsOnSubstrate { get; set; }
    public bool IsMeasurementComplete { get; set; }
    public DateTime? CompleteDate { get; set; }
    public ICollection<FrameMonitor> FrameMonitors { get; set; } = [];

}
