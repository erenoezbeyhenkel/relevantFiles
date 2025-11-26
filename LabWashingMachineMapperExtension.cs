using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.LabWashingMachine;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.HdpData.HdpWashingMachine;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class LabWashingMachineMapperExtension
{
    public static LabWashingMachineDto ToDto(this LabWashingMachine labWashingMachine) => new(labWashingMachine.Id,
                                                                                              labWashingMachine.InstrumentModelId,
                                                                                              labWashingMachine.LocationLabel,
                                                                                              labWashingMachine.InstrumentId,
                                                                                              labWashingMachine.InstrumentDescription,
                                                                                              labWashingMachine.InstrumentActiveFlag,
                                                                                              labWashingMachine.CurrentLocation,
                                                                                              labWashingMachine.LocationId,
                                                                                              labWashingMachine.MacAddress,
                                                                                              labWashingMachine.IsInHdpDeleted,
                                                                                              labWashingMachine.CreatedDateInstrument,
                                                                                              labWashingMachine.ModifiedDateInstrument,
                                                                                              labWashingMachine.CreatedAt,
                                                                                              labWashingMachine.UpdatedAt,
                                                                                              [.. labWashingMachine.LabWashingMachineMappings?.Select(lwmm => lwmm?.ToDto())]);

    public static LabWashingMachine ToLabWashingMachine(this HdpWashingMachineDto hdpWashingMachineDto) => new()
    {
        InstrumentModelId = hdpWashingMachineDto.InstrumentModelId,
        LocationLabel = hdpWashingMachineDto.LocationLabel,
        InstrumentId = hdpWashingMachineDto.InstrumentId,
        InstrumentDescription = hdpWashingMachineDto.InstrumentDescription,
        InstrumentActiveFlag = hdpWashingMachineDto.InstrumentActiveFlag,
        AdditionalInfo = hdpWashingMachineDto.AdditionalInfo,
        AssetNumber = hdpWashingMachineDto.AssetNumber,
        CostCenter = hdpWashingMachineDto.CostCenter,
        CurrentLocation = hdpWashingMachineDto.CurrentLocation,
        FactoryNumber = hdpWashingMachineDto.FactoryNumber,
        LocationId = hdpWashingMachineDto.LocationId,
        MacAddress = hdpWashingMachineDto.MacAddress.Trim().ToUpper(),
        RndOrderNumber = hdpWashingMachineDto.RndOrderNumber,
        SerialNumber = hdpWashingMachineDto.SerialNumber,
        WorkshopDeviceNumber = hdpWashingMachineDto.WorkshopDeviceNumber,
        CreatedDateInstrument = hdpWashingMachineDto.CreatedDateInstrument,
        ModifiedDateInstrument = hdpWashingMachineDto.ModifiedDateInstrument,
        CreatedDateLocation = hdpWashingMachineDto.CreatedDateLocation,
        ModifiedDateLocation = hdpWashingMachineDto.ModifiedDateLocation,
        NextCheck = hdpWashingMachineDto.NextCheck,
        EntryDate = hdpWashingMachineDto.EntryDate,
        LastCheck = hdpWashingMachineDto.LastCheck
    };

    public static void Map(this HdpWashingMachineDto hdpWashingMachineDto, LabWashingMachine entity)
    {
        entity.InstrumentModelId = hdpWashingMachineDto.InstrumentModelId;
        entity.LocationLabel = hdpWashingMachineDto.LocationLabel;
        entity.InstrumentId = hdpWashingMachineDto.InstrumentId;
        entity.InstrumentDescription = hdpWashingMachineDto.InstrumentDescription;
        entity.InstrumentActiveFlag = hdpWashingMachineDto.InstrumentActiveFlag;
        entity.AdditionalInfo = hdpWashingMachineDto.AdditionalInfo;
        entity.AssetNumber = hdpWashingMachineDto.AssetNumber;
        entity.CostCenter = hdpWashingMachineDto.CostCenter;
        entity.CurrentLocation = hdpWashingMachineDto.CurrentLocation;
        entity.FactoryNumber = hdpWashingMachineDto.FactoryNumber;
        entity.LocationId = hdpWashingMachineDto.LocationId;
        entity.MacAddress = hdpWashingMachineDto.MacAddress.Trim().ToUpper();
        entity.RndOrderNumber = hdpWashingMachineDto.RndOrderNumber;
        entity.SerialNumber = hdpWashingMachineDto.SerialNumber;
        entity.WorkshopDeviceNumber = hdpWashingMachineDto.WorkshopDeviceNumber;
        entity.CreatedDateInstrument = hdpWashingMachineDto.CreatedDateInstrument;
        entity.ModifiedDateInstrument = hdpWashingMachineDto.ModifiedDateInstrument;
        entity.CreatedDateLocation = hdpWashingMachineDto.CreatedDateLocation;
        entity.ModifiedDateLocation = hdpWashingMachineDto.ModifiedDateLocation;
        entity.NextCheck = hdpWashingMachineDto.NextCheck;
        entity.EntryDate = hdpWashingMachineDto.EntryDate;
        entity.LastCheck = hdpWashingMachineDto.LastCheck;
    }
}
