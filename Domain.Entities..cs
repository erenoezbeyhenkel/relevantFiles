using Hcb.Rnd.Pwn.Domain.Entities.Base;
using Hcb.Rnd.Pwn.Domain.Entities.Monitors;

namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments;

public class MonitorSetup : BaseEntity
{
    public long ExperimentId { get; set; }
    public Experiment Experiment { get; set; }


    public long MonitorId { get; set; }
    public Monitors.Monitor Monitor { get; set; }


    public long DeviceTypeId { get; set; }
    public DeviceType DeviceType { get; set; }


    public int NumberOfSamples { get; set; }

    public bool IsPrewashed { get; set; }

    public bool IsUvMeasurement { get; set; }

    public bool IsSoarRequested { get; set; }

    public DateTime? SoarRequestedDate { get; set; }

    public bool IsDmcAssigned { get; set; }

    public DateTime? DmcAssignedDate { get; set; }

    public bool IsMeasured { get; set; }

    public DateTime? MeasuredDate { get; set; }

    public ICollection<WashCycleSetup> WashCycleSetups { get; set; } = [];

}
