using ErrorOr;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Measurement.ColorValueCalculation.DataColor;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.FrameMonitor;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Measurement.ColorMetricCalculation.GetDataColorCalculationResults;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Measurement.DataColor;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Measurement.XyzNorm;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.ColorValueCalculation.DataColor;
public interface IDataColorCalculator
{
    double X { get; }
    double Y { get; }
    double Z { get; }
    double L { get; }
    double A { get; }
    double B { get; }
    List<XyzNormDto> XyzNormValueCollection { get; }
    List<DataColorCalculationResultDto> DataColorCalculationResultDtos { get; }
    protected Task<ErrorOr<bool>> CalculateColorValues(Dictionary<int, double> nmValues, CancellationToken cancellationToken);

    virtual Task<ErrorOr<bool>> CalulateRelevantColorValues(bool isUv, CalculateDataColorMetricsDto calculateDataColorValuesDto, CancellationToken cancellationToken)
        => Task.FromResult<ErrorOr<bool>>(true);

    Task<ErrorOr<List<GetDataColorCalculationResultsQueryDto>>> GetDataColorCalculationResults(List<FrameMonitorDto> frameMonitors, bool isUv)
        => Task.FromResult<ErrorOr<List<GetDataColorCalculationResultsQueryDto>>>(
            new List<GetDataColorCalculationResultsQueryDto>());
}
