using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Measurements;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Monitors;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Experiments.WashCycleSetUp.Create;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.WashCycleSetUp;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.WashCycleSetUp.ImageGallery;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class WashCycleSetupMapperExtension
{
    public static WashCycleSetup ToEntity(this WashCycleSetupDto dto) => new()
    {
        Id = dto.Id,
        MonitorSetupId = dto.MonitorSetupId,
        IsInitial = dto.IsInitial,
        IsMeasurementComplete = dto.IsMeasurementComplete,
        CompleteDate = dto.CompleteDate,
        WashCycle = dto.WashCycle,
        MeasurementPointsOnSubstrate = dto.MeasurementPointsOnSubstrate,
        MonitorBatchId = dto.MonitorBatchId
    };

    public static WashCycleSetup ToEntity(this CreateWashCycleSetupDto dto) => new()
    {
        MonitorSetupId = dto.MonitorSetupId,
        IsInitial = dto.IsInitial,
        IsMeasurementComplete = dto.IsMeasurementComplete,
        WashCycle = dto.WashCycle,
        MeasurementPointsOnSubstrate = dto.MeasurementPointsOnSubstrate,
        MonitorBatchId = dto.MonitorBatchId
    };

    public static WashCycleSetupDto ToDto(this WashCycleSetup entity) => new(entity.Id,
                                                                              entity.MonitorSetupId,
                                                                              entity.IsInitial,
                                                                              entity.IsMeasurementComplete,
                                                                              entity.CompleteDate,
                                                                              entity.WashCycle,
                                                                              entity.MeasurementPointsOnSubstrate,
                                                                              entity.MonitorBatchId);

    public static void Map(this WashCycleSetupDto dto, WashCycleSetup entity)
    {
        entity.MonitorSetupId = dto.MonitorSetupId;
        entity.IsInitial = dto.IsInitial;
        entity.IsMeasurementComplete = dto.IsMeasurementComplete;
        entity.CompleteDate = dto.CompleteDate;
        entity.WashCycle = dto.WashCycle;
        entity.MeasurementPointsOnSubstrate = dto.MeasurementPointsOnSubstrate;
        entity.MonitorBatchId = dto.MonitorBatchId;
    }

    public static WashCycleSetupImageGalleryDto ToImageGalleryDataDto(this WashCycleSetup washCycleSetup) => new(washCycleSetup.Id,
                                                                                                                 washCycleSetup.MeasurementPointsOnSubstrate,
                                                                                                                 washCycleSetup.FrameMonitors.Any(fm => fm.FrameSubstrates.Any(fs => !Guard.Against.IsNullOrEmpty(fs.ImageFilePath))),
                                                                                                                 washCycleSetup.FrameMonitors.FirstOrDefault()?.FrameSubstrates
                                                                                                                                             .Where(fs => !Guard.Against.IsNullOrEmpty(fs.ImageFilePath))
                                                                                                                                             .Select(fs => fs.Substrate?.ToDto())
                                                                                                                                             .ToList());
}
