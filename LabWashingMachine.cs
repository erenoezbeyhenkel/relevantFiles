using Hcb.Rnd.Pwn.Domain.Entities.Base;

namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments;

public sealed class LabWashingMachine : BaseEntity
{
    public string InstrumentModelId { get; set; }
    public string LocationLabel { get; set; }
    public string InstrumentId { get; set; }
    public string InstrumentDescription { get; set; }
    public char InstrumentActiveFlag { get; set; }
    public string AdditionalInfo { get; set; }
    public string AssetNumber { get; set; }
    public string CostCenter { get; set; }
    public string CurrentLocation { get; set; }
    public string FactoryNumber { get; set; }
    public string LocationId { get; set; }
    public string MacAddress { get; set; }
    public string RndOrderNumber { get; set; }
    public string SerialNumber { get; set; }
    public string WorkshopDeviceNumber { get; set; }
    public bool IsInHdpDeleted { get; set; }


    public DateTime CreatedDateInstrument { get; set; }
    public DateTime ModifiedDateInstrument { get; set; }
    public DateTime? CreatedDateLocation { get; set; }
    public DateTime? ModifiedDateLocation { get; set; }
    public DateTime? NextCheck { get; set; }
    public DateTime? EntryDate { get; set; }
    public DateTime? LastCheck { get; set; }
    public ICollection<LabWashingMachineMapping> LabWashingMachineMappings { get; set; } = [];
}
