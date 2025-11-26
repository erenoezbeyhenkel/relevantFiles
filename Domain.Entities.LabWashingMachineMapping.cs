using Hcb.Rnd.Pwn.Domain.Entities.Base;

namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments;

public sealed class LabWashingMachineMapping : BaseEntity
{
    public long LabWashingMachineId { get; set; }
    public LabWashingMachine LabWashingMachine { get; set; }

    public long WashingMachineId { get; set; }
    public WashingMachine WashingMachine { get; set; }
}
