using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Base;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions;

public static class ExperimentExtensions
{
    public static string GetExperimentLastUpdatedById(this Experiment experiment)
    {
        if (Guard.Against.IsNull(experiment))
            return string.Empty;

        var allObjects = new List<(BaseEntity Entity, DateTime? UpdatedAt)>();

        allObjects.AddRange(experiment.MonitorSetups?.Select(ms => (ms as BaseEntity, ms?.UpdatedAt)));
        allObjects.AddRange(experiment.MonitorSetups?.SelectMany(ms => ms.WashCycleSetups).Select(wcs => (wcs as BaseEntity, wcs?.UpdatedAt)));
        allObjects.AddRange(experiment.ProductSetups?.Select(ps => (ps as BaseEntity, ps?.UpdatedAt)));
        allObjects.AddRange(experiment.ProductSetups?.SelectMany(ps => ps.Additives).Select(psa => (psa as BaseEntity, psa?.UpdatedAt)));

        var (Entity, UpdatedAt) = allObjects
            .OrderByDescending(x => x.UpdatedAt.Value)
            .FirstOrDefault();

        return Entity.UpdatedById;
    }

    public static string GetExperimentTemplateLastUpdatedById(this Experiment experiment)
    {
        if (Guard.Against.IsNull(experiment))
            return string.Empty;

        var allObjects = new List<(BaseEntity Entity, DateTime? UpdatedAt)>();

        allObjects.AddRange(experiment.MonitorSetups?.Select(ms => (ms as BaseEntity, ms?.UpdatedAt)));
        allObjects.AddRange(experiment.MonitorSetups?.SelectMany(ms => ms.WashCycleSetups).Select(wcs => (wcs as BaseEntity, wcs?.UpdatedAt)));

        allObjects.Add((experiment.TemplateSetting, experiment.TemplateSetting?.UpdatedAt));
        allObjects.AddRange(experiment.TemplateAdditives?.Select(ta => (ta as BaseEntity, ta?.UpdatedAt)));
        allObjects.AddRange(experiment.AssignedExperimentTemplateGroups?.Select(aetg => (aetg as BaseEntity, aetg?.UpdatedAt)));

        var (Entity, UpdatedAt) = allObjects
            .OrderByDescending(x => x.UpdatedAt.Value)
            .FirstOrDefault();

        return Entity.UpdatedById;
    }

    public static string GetAzureBlogStorageContainerName(this Experiment experiment, MonitorSetup monitorSetup, WashCycleSetup washCycleSetup)
    {
        if (experiment.ExperimentTypeId.IsPillingTest())
            return $"eid{experiment.Id}wc{washCycleSetup.WashCycle}";

        if (!Guard.Against.IsNull(monitorSetup) && monitorSetup.DeviceTypeId.IsUSRobot())
            return $"eid{experiment.Id}-usrobot";

        if (!Guard.Against.IsNull(monitorSetup) && monitorSetup.DeviceTypeId.IsExternalExcelUpload() && experiment.IsExternal)
            return $"eid{experiment.Id}-external-excel-upload";

        if (!Guard.Against.IsNull(monitorSetup) && monitorSetup.DeviceTypeId.IsExternalMach5Upload() && experiment.IsExternal)
            return $"eid{experiment.Id}-external-mach5-upload";

        return string.Empty;
    }
}
