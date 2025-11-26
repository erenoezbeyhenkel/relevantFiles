using ErrorOr;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Measurement.ColorMetric;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.ColorValueCalculation.Mach5;
public interface IMach5Calculator
{
    protected Task<ErrorOr<ColorMetricDto>> CalculateColorValues(bool isXyzCalculationNeeded, ColorMetricDto colorMetricDto, CancellationToken cancellationToken);

    virtual Task<ErrorOr<List<ColorMetricDto>>> CalulateRelevantColorValues(long washCycleSetupId, List<ColorMetricDto> colorMetricDtos, CancellationToken cancellationToken)
        => Task.FromResult<ErrorOr<List<ColorMetricDto>>>(default);
}
