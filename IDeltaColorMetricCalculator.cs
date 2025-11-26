using ErrorOr;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Measurement.ColorMetric;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Measurement.DeltaColorMetric;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.ColorValueCalculation.DeltaColorMetric;
public interface IDeltaColorMetricCalculator
{
    Task<ErrorOr<DeltaColorMetricDto>> CalculateDeltaColorMetrics(long? frameSubstrateId, int? measurementPoint, bool isQuickMeasurement, ColorMetricDto newColorMetric, List<ColorMetricDto> initialColorMetrics, CancellationToken cancellationToken);
    Task<ErrorOr<DeltaColorMetricDto>> CalculateMach5DeltaColorMetrics(long frameSubstrateId, int measurementPoint, ColorMetricDto newColorMetric, List<ColorMetricDto> initialColorMetrics, CancellationToken cancellationToken);

}
