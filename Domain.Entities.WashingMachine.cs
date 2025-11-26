using Hcb.Rnd.Pwn.Domain.Entities.Base;

namespace Hcb.Rnd.Pwn.Domain.Entities.Experiments;

public class WashingMachine : BaseEntity
{
    public string Name { get; set; }// Same value with InstrumentModelId in LabWashingMachine
    public string Program { get; set; }
    public string Temperature { get; set; }
    public int MainWashTime { get; set; }
    public int TotalWashTime { get; set; }// if this is null then take the main wash time.

    #region Hdp fields
    public char ActiveFlag { get; set; }
    public char DrainPumpEquipped { get; set; }
    public string LoadingType { get; set; }
    public string Manufacturer { get; set; }
    public string MaximumLoad { get; set; }
    public char NeedsHotWater { get; set; }
    public string ProgramListId { get; set; }
    public int UserSequence { get; set; }
    public DateTime CreatedDateInstrumentModel { get; set; }
    public DateTime ModifiedDateInstrumentModel { get; set; }
    public DateTime CreatedDateProgramList { get; set; }
    public DateTime ModifiedDateProgramList { get; set; }
    public bool IsInHdpDeleted { get; set; }
    #endregion

    public ICollection<ProductSetup> ProductSetups { get; set; } = [];
    public ICollection<LabWashingMachineMapping> LabWashingMachineMappings { get; set; } = [];

}
