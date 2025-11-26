using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Monitors;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.MonitorSetUp.Create;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.MonitorSetUp.Update;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.MonitorSetUp;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class MonitorSetupMapperExtension
{
    public static MonitorSetup ToEntity(this MonitorSetupDto dto) => new()
    {
        Id = dto.Id,
        ExperimentId = dto.ExperimentId,
        MonitorId = dto.MonitorId,
        DeviceTypeId = dto.DeviceTypeId,
        DeviceType = dto.DeviceType?.ToEntity(),
        NumberOfSamples = dto.NumberOfSamples,
        IsPrewashed = dto.IsPrewashed,
        IsUvMeasurement = dto.IsUvMeasurement,
        IsSoarRequested = dto.IsSoarRequested,
        IsDmcAssigned = dto.IsDmcAssigned,
        IsMeasured = dto.IsMeasured,
        MeasuredDate = dto.MeasuredDate,
        SoarRequestedDate = dto.SoarRequestedDate,
        WashCycleSetups = dto.WashCycleSetups?.Select(wss => wss?.ToEntity()).ToList()
    };

    public static MonitorSetup ToEntity(this CreateMonitorSetupDto dto) => new()
    {
        ExperimentId = dto.ExperimentId,
        MonitorId = dto.MonitorId,
        DeviceTypeId = dto.DeviceTypeId,
        NumberOfSamples = dto.NumberOfSamples,
        IsPrewashed = dto.IsPrewashed,
        IsUvMeasurement = dto.IsUvMeasurement
    };

    public static MonitorSetupDto ToDto(this MonitorSetup entity) => new(entity.Id,
                                                                          entity.ExperimentId,
                                                                          entity.MonitorId,
                                                                          $"{entity.Monitor?.Name} [{entity.Monitor?.MonitorType?.Name}] [{entity.Id}]",
                                                                          entity.Monitor.Acronym,
                                                                          entity.Monitor.MonitorTypeId,
                                                                          entity.DeviceTypeId,
                                                                          entity.DeviceType?.ToDto(),
                                                                          entity.NumberOfSamples,
                                                                          entity.IsPrewashed,
                                                                          entity.IsUvMeasurement,
                                                                          entity.IsSoarRequested,
                                                                          entity.IsDmcAssigned,
                                                                          entity.IsMeasured,
                                                                          entity.MeasuredDate,
                                                                          entity.SoarRequestedDate,
                                                                          entity.WashCycleSetups?.Select(wss => wss?.ToDto())?.ToList());

    public static UpdateMonitorSetupDto ToUpdateMonitorSetupDto(this MonitorSetup entity) => new(entity.Id,
                                                                                                 entity.ExperimentId,
                                                                                                 entity.MonitorId,
                                                                                                 entity.DeviceTypeId,
                                                                                                 entity.NumberOfSamples,
                                                                                                 entity.IsPrewashed,
                                                                                                 entity.IsUvMeasurement,
                                                                                                 entity.IsSoarRequested,
                                                                                                 entity.IsDmcAssigned,
                                                                                                 entity.IsMeasured,
                                                                                                 entity.MeasuredDate,
                                                                                                 entity.SoarRequestedDate);

    public static void Map(this UpdateMonitorSetupDto dto, MonitorSetup entity)
    {
        entity.ExperimentId = dto.ExperimentId;
        entity.MonitorId = dto.MonitorId;
        entity.DeviceTypeId = dto.DeviceTypeId;
        entity.NumberOfSamples = dto.NumberOfSamples;
        entity.IsPrewashed = dto.IsPrewashed;
        entity.IsUvMeasurement = dto.IsUvMeasurement;
        entity.IsSoarRequested = dto.IsSoarRequested;
        entity.IsSoarRequested = dto.IsSoarRequested;
        entity.IsDmcAssigned = dto.IsDmcAssigned;
        entity.IsMeasured = dto.IsMeasured;
        entity.MeasuredDate = dto.MeasuredDate;
        entity.SoarRequestedDate = dto.SoarRequestedDate;
    }
}
