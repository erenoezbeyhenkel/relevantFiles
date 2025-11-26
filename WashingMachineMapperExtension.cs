using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.WashingMachine;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.HdpData.HdpWashingMachineModelProgram;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class WashingMachineMapperExtension
{
    public static WashingMachine ToEntity(this WashingMachineDto dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name,
        Program = dto.Program,
        Temperature = dto.Temperature,
        MainWashTime = dto.MainWashTime,
        TotalWashTime = dto.TotalWashTime
    };

    public static WashingMachineDto ToDto(this WashingMachine entity) => new(entity.Id,
                                                                             entity.Name,
                                                                             entity.Program,
                                                                             entity.Temperature,
                                                                             entity.MainWashTime,
                                                                             entity.TotalWashTime,
                                                                             entity.LoadingType,
                                                                             [.. entity.LabWashingMachineMappings.Select(x => x.LabWashingMachine?.ToDto())]);

    public static void Map(this WashingMachineDto dto, WashingMachine entity)
    {
        entity.Name = dto.Name;
        entity.Program = dto.Program;
        entity.Temperature = dto.Temperature;
        entity.MainWashTime = dto.MainWashTime;
        entity.TotalWashTime = dto.TotalWashTime;
    }

    public static void Map(this HdpWashingMachineModelProgramDto hdpWashingMachineModelProgramDto, WashingMachine entity)
    {
        entity.Name = hdpWashingMachineModelProgramDto.InstrumentModelId;
        entity.Program = hdpWashingMachineModelProgramDto.Program;
        entity.Temperature = hdpWashingMachineModelProgramDto.Temperature;
        entity.MainWashTime = hdpWashingMachineModelProgramDto.MainWashTime;
        entity.TotalWashTime = hdpWashingMachineModelProgramDto.TotalWashTime;
        entity.ActiveFlag = hdpWashingMachineModelProgramDto.ActiveFlag;
        entity.DrainPumpEquipped = hdpWashingMachineModelProgramDto.DrainPumpEquipped;
        entity.LoadingType = hdpWashingMachineModelProgramDto.LoadingType;
        entity.Manufacturer = hdpWashingMachineModelProgramDto.Manufacturer;
        entity.MaximumLoad = hdpWashingMachineModelProgramDto.MaximumLoad;
        entity.NeedsHotWater = hdpWashingMachineModelProgramDto.NeedsHotWater;
        entity.ProgramListId = hdpWashingMachineModelProgramDto.ProgramListId;
        entity.UserSequence = hdpWashingMachineModelProgramDto.UserSequence;
        entity.CreatedDateInstrumentModel = hdpWashingMachineModelProgramDto.CreatedDateInstrumentModel;
        entity.ModifiedDateInstrumentModel = hdpWashingMachineModelProgramDto.ModifiedDateInstrumentModel;
        entity.CreatedDateProgramList = hdpWashingMachineModelProgramDto.CreatedDateProgramList;
        entity.ModifiedDateProgramList = hdpWashingMachineModelProgramDto.ModifiedDateProgramList;
    }

    public static WashingMachine ToWashingMachine(this HdpWashingMachineModelProgramDto hdpWashingMachineModelProgramDto) => new()
    {
        Name = hdpWashingMachineModelProgramDto.InstrumentModelId,
        Program = hdpWashingMachineModelProgramDto.Program,
        Temperature = hdpWashingMachineModelProgramDto.Temperature,
        MainWashTime = hdpWashingMachineModelProgramDto.MainWashTime,
        TotalWashTime = hdpWashingMachineModelProgramDto.TotalWashTime,
        ActiveFlag = hdpWashingMachineModelProgramDto.ActiveFlag,
        DrainPumpEquipped = hdpWashingMachineModelProgramDto.DrainPumpEquipped,
        LoadingType = hdpWashingMachineModelProgramDto.LoadingType,
        Manufacturer = hdpWashingMachineModelProgramDto.Manufacturer,
        MaximumLoad = hdpWashingMachineModelProgramDto.MaximumLoad,
        NeedsHotWater = hdpWashingMachineModelProgramDto.NeedsHotWater,
        ProgramListId = hdpWashingMachineModelProgramDto.ProgramListId,
        UserSequence = hdpWashingMachineModelProgramDto.UserSequence,
        CreatedDateInstrumentModel = hdpWashingMachineModelProgramDto.CreatedDateInstrumentModel,
        ModifiedDateInstrumentModel = hdpWashingMachineModelProgramDto.ModifiedDateInstrumentModel,
        CreatedDateProgramList = hdpWashingMachineModelProgramDto.CreatedDateProgramList,
        ModifiedDateProgramList = hdpWashingMachineModelProgramDto.ModifiedDateProgramList,
    };
}
