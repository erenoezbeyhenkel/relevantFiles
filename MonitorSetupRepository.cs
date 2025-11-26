using Hcb.Rnd.Pwn.Application.Interfaces.Persistence.Repositories.Experiments;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Domain.Entities.Measurements;
using Hcb.Rnd.Pwn.Infrastructure.Persistence.Base;
using Microsoft.EntityFrameworkCore;

namespace Hcb.Rnd.Pwn.Infrastructure.Persistence.Repositories.Experiments;

public sealed class MonitorSetupRepository(DataBaseContext dataBaseContext) : GenericRepository<MonitorSetup>(dataBaseContext), IMonitorSetupRepository
{
    public async Task<List<string>> GetNonZeroColorMetricFieldsAsync(long id, CancellationToken cancellationToken)
    {
        var colorMetrics = await Query
            .Where(ms => ms.Id == id)
            .SelectMany(ms => ms.WashCycleSetups)
            .SelectMany(wcs => wcs.FrameMonitors)
            .SelectMany(fm => fm.FrameSubstrates)
            .SelectMany(fs => fs.ColorMetrics)
            .Select(cm => new { cm.X, cm.Y, cm.Z, cm.L, cm.A, cm.B })
            .ToListAsync(cancellationToken);

        var substrateDefaultColorMetrics = await Query
            .Where(ms => ms.Id == id)
            .SelectMany(ms => ms.WashCycleSetups)
            .Select(ws => ws.MonitorBatch)
            .SelectMany(wcs => wcs.BatchFrameMonitors)
            .SelectMany(fm => fm.BatchFrameSubstrates)
            .SelectMany(fs => fs.SubstrateDefaultColorMetrics)
            .Select(cm => new { cm.X, cm.Y, cm.Z, cm.L, cm.A, cm.B })
            .ToListAsync(cancellationToken);

        if (!Guard.Against.IsAnyOrNotEmpty(colorMetrics) && !Guard.Against.IsAnyOrNotEmpty(substrateDefaultColorMetrics))
            return [];

        var result = new List<string>();

        if (colorMetrics.Any(cm => cm.X > 0) || substrateDefaultColorMetrics.Any(cm => cm.X > 0)) result.Add(nameof(ColorMetric.X));
        if (colorMetrics.Any(cm => cm.Y > 0) || substrateDefaultColorMetrics.Any(cm => cm.Y > 0)) result.Add(nameof(ColorMetric.Y));
        if (colorMetrics.Any(cm => cm.Z > 0) || substrateDefaultColorMetrics.Any(cm => cm.Z > 0)) result.Add(nameof(ColorMetric.Z));
        if (colorMetrics.Any(cm => cm.L > 0) || substrateDefaultColorMetrics.Any(cm => cm.L > 0)) result.Add(nameof(ColorMetric.L));
        if (colorMetrics.Any(cm => cm.A > 0) || substrateDefaultColorMetrics.Any(cm => cm.A > 0)) result.Add(nameof(ColorMetric.A));
        if (colorMetrics.Any(cm => cm.B > 0) || substrateDefaultColorMetrics.Any(cm => cm.B > 0)) result.Add(nameof(ColorMetric.B));

        return result;
    }
    public async Task<List<string>> GetNonZeroDeltaColorMetricFieldsAsync(long id, CancellationToken cancellationToken)
    {
        var deltaColorMetrics = await Query
            .Where(ms => ms.Id == id)
            .SelectMany(ms => ms.WashCycleSetups)
            .SelectMany(wcs => wcs.FrameMonitors)
            .SelectMany(fm => fm.FrameSubstrates)
            .SelectMany(fs => fs.DeltaColorMetrics)
            .Select(cm => new { cm.dX, cm.dY, cm.dZ, cm.dL, cm.da, cm.db, cm.dE, cm.dEF, cm.dE_GS, cm.SSRinternal, cm.SSR, cm.RSI, cm.SRI })
            .ToListAsync(cancellationToken);

        if (!Guard.Against.IsAnyOrNotEmpty(deltaColorMetrics))
            return [];

        var result = new List<string>();

        if (deltaColorMetrics.Any(cm => cm.dX > 0)) result.Add(nameof(DeltaColorMetric.dX));
        if (deltaColorMetrics.Any(cm => cm.dY > 0)) result.Add(nameof(DeltaColorMetric.dY));
        if (deltaColorMetrics.Any(cm => cm.dZ > 0)) result.Add(nameof(DeltaColorMetric.dZ));
        if (deltaColorMetrics.Any(cm => cm.dL > 0)) result.Add(nameof(DeltaColorMetric.dL));
        if (deltaColorMetrics.Any(cm => cm.da > 0)) result.Add(nameof(DeltaColorMetric.da));
        if (deltaColorMetrics.Any(cm => cm.db > 0)) result.Add(nameof(DeltaColorMetric.db));
        if (deltaColorMetrics.Any(cm => cm.dE > 0)) result.Add(nameof(DeltaColorMetric.dE));
        if (deltaColorMetrics.Any(cm => cm.dEF > 0)) result.Add(nameof(DeltaColorMetric.dEF));
        if (deltaColorMetrics.Any(cm => cm.dE_GS > 0)) result.Add(nameof(DeltaColorMetric.dE_GS));
        if (deltaColorMetrics.Any(cm => cm.SSRinternal.HasValue)) result.Add(nameof(DeltaColorMetric.SSRinternal));
        if (deltaColorMetrics.Any(cm => cm.SSR.HasValue)) result.Add(nameof(DeltaColorMetric.SSR));
        if (deltaColorMetrics.Any(cm => cm.RSI.HasValue)) result.Add(nameof(DeltaColorMetric.RSI));
        if (deltaColorMetrics.Any(cm => cm.SRI.HasValue)) result.Add(nameof(DeltaColorMetric.SRI));

        return result;
    }

    public async Task<List<string>> GetNonZeroUvColorMetricFieldsAsync(long id, CancellationToken cancellationToken)
    {
        var uvColorMetrics = await Query
            .Where(ms => ms.Id == id)
            .SelectMany(ms => ms.WashCycleSetups)
            .SelectMany(wcs => wcs.FrameMonitors)
            .SelectMany(fm => fm.FrameSubstrates)
            .SelectMany(fs => fs.UvColorMetrics)
            .Select(cm => new { cm.Berger, cm.Ganz, cm.Faz, cm.GCIE })
            .ToListAsync(cancellationToken);

        if (!Guard.Against.IsAnyOrNotEmpty(uvColorMetrics))
            return [];

        var result = new List<string>();

        if (uvColorMetrics.Any(cm => cm.Berger.HasValue)) result.Add(nameof(UvColorMetric.Berger));
        if (uvColorMetrics.Any(cm => cm.Ganz.HasValue)) result.Add(nameof(UvColorMetric.Ganz));
        if (uvColorMetrics.Any(cm => cm.Faz.HasValue)) result.Add(nameof(UvColorMetric.Faz));
        if (uvColorMetrics.Any(cm => cm.GCIE.HasValue)) result.Add(nameof(UvColorMetric.GCIE));

        return result;
    }

    public async Task<List<string>> GetNonZeroRadiationWavelengthFieldsAsync(long id, CancellationToken cancellationToken)
    {
        var radiationWavelengths = await Query
            .Where(ms => ms.Id == id)
            .SelectMany(ms => ms.WashCycleSetups)
            .SelectMany(wcs => wcs.FrameMonitors)
            .SelectMany(fm => fm.FrameSubstrates)
            .SelectMany(fs => fs.RadiationWavelengths)
            .Select(cm => new
            {
                cm.Nm360,
                cm.Nm370,
                cm.Nm380,
                cm.Nm390,
                cm.Nm400,
                cm.Nm410,
                cm.Nm420,
                cm.Nm430,
                cm.Nm440,
                cm.Nm450,
                cm.Nm460,
                cm.Nm470,
                cm.Nm480,
                cm.Nm490,
                cm.Nm500,
                cm.Nm510,
                cm.Nm520,
                cm.Nm530,
                cm.Nm540,
                cm.Nm550,
                cm.Nm560,
                cm.Nm570,
                cm.Nm580,
                cm.Nm590,
                cm.Nm600,
                cm.Nm610,
                cm.Nm620,
                cm.Nm630,
                cm.Nm640,
                cm.Nm650,
                cm.Nm660,
                cm.Nm670,
                cm.Nm680,
                cm.Nm690,
                cm.Nm700
            })
            .ToListAsync(cancellationToken);

        if (!Guard.Against.IsAnyOrNotEmpty(radiationWavelengths))
            return [];

        var result = new List<string>();

        if (radiationWavelengths.Any(cm => cm.Nm360.HasValue)) result.Add(nameof(RadiationWavelength.Nm360));
        if (radiationWavelengths.Any(cm => cm.Nm370.HasValue)) result.Add(nameof(RadiationWavelength.Nm370));
        if (radiationWavelengths.Any(cm => cm.Nm380.HasValue)) result.Add(nameof(RadiationWavelength.Nm380));
        if (radiationWavelengths.Any(cm => cm.Nm390.HasValue)) result.Add(nameof(RadiationWavelength.Nm390));
        if (radiationWavelengths.Any(cm => cm.Nm400.HasValue)) result.Add(nameof(RadiationWavelength.Nm400));
        if (radiationWavelengths.Any(cm => cm.Nm410.HasValue)) result.Add(nameof(RadiationWavelength.Nm410));
        if (radiationWavelengths.Any(cm => cm.Nm420.HasValue)) result.Add(nameof(RadiationWavelength.Nm420));
        if (radiationWavelengths.Any(cm => cm.Nm430.HasValue)) result.Add(nameof(RadiationWavelength.Nm430));
        if (radiationWavelengths.Any(cm => cm.Nm440.HasValue)) result.Add(nameof(RadiationWavelength.Nm440));
        if (radiationWavelengths.Any(cm => cm.Nm450.HasValue)) result.Add(nameof(RadiationWavelength.Nm450));
        if (radiationWavelengths.Any(cm => cm.Nm460.HasValue)) result.Add(nameof(RadiationWavelength.Nm460));
        if (radiationWavelengths.Any(cm => cm.Nm470.HasValue)) result.Add(nameof(RadiationWavelength.Nm470));
        if (radiationWavelengths.Any(cm => cm.Nm480.HasValue)) result.Add(nameof(RadiationWavelength.Nm480));
        if (radiationWavelengths.Any(cm => cm.Nm490.HasValue)) result.Add(nameof(RadiationWavelength.Nm490));
        if (radiationWavelengths.Any(cm => cm.Nm500.HasValue)) result.Add(nameof(RadiationWavelength.Nm500));
        if (radiationWavelengths.Any(cm => cm.Nm510.HasValue)) result.Add(nameof(RadiationWavelength.Nm510));
        if (radiationWavelengths.Any(cm => cm.Nm520.HasValue)) result.Add(nameof(RadiationWavelength.Nm520));
        if (radiationWavelengths.Any(cm => cm.Nm530.HasValue)) result.Add(nameof(RadiationWavelength.Nm530));
        if (radiationWavelengths.Any(cm => cm.Nm540.HasValue)) result.Add(nameof(RadiationWavelength.Nm540));
        if (radiationWavelengths.Any(cm => cm.Nm550.HasValue)) result.Add(nameof(RadiationWavelength.Nm550));
        if (radiationWavelengths.Any(cm => cm.Nm560.HasValue)) result.Add(nameof(RadiationWavelength.Nm560));
        if (radiationWavelengths.Any(cm => cm.Nm570.HasValue)) result.Add(nameof(RadiationWavelength.Nm570));
        if (radiationWavelengths.Any(cm => cm.Nm580.HasValue)) result.Add(nameof(RadiationWavelength.Nm580));
        if (radiationWavelengths.Any(cm => cm.Nm590.HasValue)) result.Add(nameof(RadiationWavelength.Nm590));
        if (radiationWavelengths.Any(cm => cm.Nm600.HasValue)) result.Add(nameof(RadiationWavelength.Nm600));
        if (radiationWavelengths.Any(cm => cm.Nm610.HasValue)) result.Add(nameof(RadiationWavelength.Nm610));
        if (radiationWavelengths.Any(cm => cm.Nm620.HasValue)) result.Add(nameof(RadiationWavelength.Nm620));
        if (radiationWavelengths.Any(cm => cm.Nm630.HasValue)) result.Add(nameof(RadiationWavelength.Nm630));
        if (radiationWavelengths.Any(cm => cm.Nm640.HasValue)) result.Add(nameof(RadiationWavelength.Nm640));
        if (radiationWavelengths.Any(cm => cm.Nm650.HasValue)) result.Add(nameof(RadiationWavelength.Nm650));
        if (radiationWavelengths.Any(cm => cm.Nm660.HasValue)) result.Add(nameof(RadiationWavelength.Nm660));
        if (radiationWavelengths.Any(cm => cm.Nm670.HasValue)) result.Add(nameof(RadiationWavelength.Nm670));
        if (radiationWavelengths.Any(cm => cm.Nm680.HasValue)) result.Add(nameof(RadiationWavelength.Nm680));
        if (radiationWavelengths.Any(cm => cm.Nm690.HasValue)) result.Add(nameof(RadiationWavelength.Nm690));
        if (radiationWavelengths.Any(cm => cm.Nm700.HasValue)) result.Add(nameof(RadiationWavelength.Nm700));


        return result;
    }

    public async Task<bool> HasColorMetricDataAsync(long id, CancellationToken cancellationToken)
    {
        return await Query
            .Where(ms => ms.Id == id)
            .Select(ms => ms.WashCycleSetups
                .SelectMany(wcs => wcs.FrameMonitors)
                .SelectMany(fm => fm.FrameSubstrates)
                .SelectMany(fs => fs.ColorMetrics)
                .Any()).FirstOrDefaultAsync(cancellationToken);

    }


    public async Task<bool> HasDeltaColorMetricDataAsync(long id, CancellationToken cancellationToken)
    {
        return await Query
            .Where(ms => ms.Id == id)
            .Select(ms => ms.WashCycleSetups
                .SelectMany(wcs => wcs.FrameMonitors)
                .SelectMany(fm => fm.FrameSubstrates)
                .SelectMany(fs => fs.DeltaColorMetrics)
                .Any()).FirstOrDefaultAsync(cancellationToken);

    }

    public async Task<bool> HasUvColorMetricDataAsync(long id, CancellationToken cancellationToken)
    {
        return await Query
            .Where(ms => ms.Id == id)
            .Select(ms => ms.WashCycleSetups
                .SelectMany(wcs => wcs.FrameMonitors)
                .SelectMany(fm => fm.FrameSubstrates)
                .SelectMany(fs => fs.UvColorMetrics)
                .Any()).FirstOrDefaultAsync(cancellationToken);

    }

    public async Task<bool> HasRadiationWavelengthDataAsync(long id, CancellationToken cancellationToken)
    {
        return await Query
            .Where(ms => ms.Id == id)
            .Select(ms => ms.WashCycleSetups
                .SelectMany(wcs => wcs.FrameMonitors)
                .SelectMany(fm => fm.FrameSubstrates)
                .SelectMany(fs => fs.RadiationWavelengths)
                .Any()).FirstOrDefaultAsync(cancellationToken);

    }
}
