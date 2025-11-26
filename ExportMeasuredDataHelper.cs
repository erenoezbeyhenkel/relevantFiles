using ClosedXML.Excel;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.DataExports.MeasuredData.Export;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;
using Hcb.Rnd.Pwn.Domain.Entities.Measurements;
using System.Linq;
using System.Reflection;

namespace Hcb.Rnd.Pwn.Application.Common.Helpers;

public static class ExportMeasuredDataHelper
{
    public static List<string> GetColorMetricProperties()
        => [.. typeof(ColorMetric)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(double?) || p.PropertyType == typeof(double))
                .Select(p => p.Name)];

    public static List<string> GetDeltaColorMetricProperties()
    => [.. typeof(DeltaColorMetric)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(double?) || p.PropertyType == typeof(double))
                .Select(p => p.Name)];

    public static List<string> GetUvColorMetricProperties()
    => [.. typeof(UvColorMetric)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(double?) || p.PropertyType == typeof(double))
                .Select(p => p.Name)];

    public static List<string> GetRadiationWavelengthProperties()
    => [.. typeof(RadiationWavelength)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(double?) || p.PropertyType == typeof(double))
                .Select(p => p.Name)];

    public delegate List<CalculatedAverageValuesDto> MetricCalculatorDelegate(IEnumerable<FrameMonitor> frameMonitors,
                                                                              IEnumerable<long> substrateIds,
                                                                              PropertyInfo propertyInfo);

    public static readonly List<(List<string> Properties, Type Type, MetricCalculatorDelegate Calculator)> MetricHandlers
        =
        [
            (GetColorMetricProperties(), typeof(ColorMetric), CalculateColorMetricAverages),
            (GetDeltaColorMetricProperties(), typeof(DeltaColorMetric), CalculateDeltaColorMetricAverages),
            (GetUvColorMetricProperties(), typeof(UvColorMetric), CalculateUvColorMetricAverages),
            (GetRadiationWavelengthProperties(), typeof(RadiationWavelength), CalculateRadiationWavelengthAverages)
        ];
    public static List<CalculatedAverageValuesDto> CalculateColorMetricAverages(IEnumerable<FrameMonitor> frameMonitors,
                                                                                 IEnumerable<long> targetSubstrateIds,
                                                                                 PropertyInfo propertyInfo)
    {
        return [.. frameMonitors
            .Where(fm => !Guard.Against.IsNull(fm.ProductSetup) && !Guard.Against.IsNull(fm.ProductSetup.Product))
            .SelectMany(fm => fm.FrameSubstrates, (fm, fs) => new { fm, fs })
            .SelectMany(temp => temp.fs.ColorMetrics, (temp, cm) => new
            {
                ProductPosition = temp.fm.ProductSetup.Position,
                ProductDescription = temp.fm.ProductSetup.Product.Description,
                temp.fs.SubstrateId,
                ColorMetricValue = propertyInfo.GetValue(cm),
                FormattedProductDescription = $"{temp.fm.ProductSetup.Position.GetFormattedPosition()} - {temp.fm.ProductSetup.Product.Description}"
            })
            .Where(x => targetSubstrateIds.Contains(x.SubstrateId))
            .OrderBy(x => x.ProductPosition)
            .GroupBy(x => x.FormattedProductDescription)
            .Select(g => new CalculatedAverageValuesDto(
                g.Key,
                targetSubstrateIds.ToDictionary(id => id,
                    id => Math.Round(g.Where(x => x.SubstrateId == id)
                                      .Average(x => (double?)x.ColorMetricValue) ?? 0, 2))
            ))];
    }



    public static List<CalculatedAverageValuesDto> CalculateSubstrateDefaultColorMetricAverages(IEnumerable<BatchFrameMonitor> frameMonitors,
                                                                                                IEnumerable<long> targetSubstrateIds,
                                                                                                PropertyInfo propertyInfo)
    {
        return [.. frameMonitors
            .SelectMany(fm => fm.BatchFrameSubstrates, (fm, fs) => new { fm, fs })
            .SelectMany(temp => temp.fs.SubstrateDefaultColorMetrics, (temp, cm) => new
            {
                ProductDescription = "Batch Is In Use",
                temp.fs.SubstrateId,
                SubstrateDefaultColorMetric = propertyInfo.GetValue(cm)
            })
            .Where(x => targetSubstrateIds.Contains(x.SubstrateId))
            .GroupBy(x => x.ProductDescription)
            .Select(g => new CalculatedAverageValuesDto(g.Key, targetSubstrateIds.ToDictionary(id => id,
                    id => Math.Round(g.Where(x => x.SubstrateId == id)
                                      .Average(x => (double?)x.SubstrateDefaultColorMetric) ?? 0, 2))))
            ];
    }

    public static List<CalculatedAverageValuesDto> CalculateDeltaColorMetricAverages(IEnumerable<FrameMonitor> frameMonitors,
                                                                                     IEnumerable<long> targetSubstrateIds,
                                                                                     PropertyInfo propertyInfo)
    {
        return [.. frameMonitors
            .Where(fm => !Guard.Against.IsNull(fm.ProductSetup) && !Guard.Against.IsNull(fm.ProductSetup.Product))
            .SelectMany(fm => fm.FrameSubstrates, (fm, fs) => new { fm, fs })
            .SelectMany(temp => temp.fs.DeltaColorMetrics, (temp, cm) => new
            {
                ProductPosition = temp.fm.ProductSetup.Position,
                ProductDescription = temp.fm.ProductSetup.Product.Description,
                temp.fs.SubstrateId,
                DeltaColorMetricValue = propertyInfo.GetValue(cm),
                FormattedProductDescription = $"{temp.fm.ProductSetup.Position.GetFormattedPosition()} - {temp.fm.ProductSetup.Product.Description}"
            })
            .Where(x => targetSubstrateIds.Contains(x.SubstrateId))
            .OrderBy(x => x.ProductPosition)
            .GroupBy(x => x.FormattedProductDescription)
            .Select(g => new CalculatedAverageValuesDto(g.Key, targetSubstrateIds.ToDictionary(id => id,
                    id => Math.Round(g.Where(x => x.SubstrateId == id)
                                      .Average(x => (double?)x.DeltaColorMetricValue) ?? 0, 2))))
            ];
    }

    public static List<CalculatedAverageValuesDto> CalculateUvColorMetricAverages(IEnumerable<FrameMonitor> frameMonitors,
                                                                                  IEnumerable<long> targetSubstrateIds,
                                                                                  PropertyInfo propertyInfo)
    {
        return [.. frameMonitors
            .Where(fm => !Guard.Against.IsNull(fm.ProductSetup) && !Guard.Against.IsNull(fm.ProductSetup.Product))
            .SelectMany(fm => fm.FrameSubstrates, (fm, fs) => new { fm, fs })
            .SelectMany(temp => temp.fs.UvColorMetrics, (temp, cm) => new
            {
                ProductPosition = temp.fm.ProductSetup.Position,
                ProductDescription = temp.fm.ProductSetup.Product.Description,
                temp.fs.SubstrateId,
                UvColorMetricValue = propertyInfo.GetValue(cm),
                FormattedProductDescription = $"{temp.fm.ProductSetup.Position.GetFormattedPosition()} - {temp.fm.ProductSetup.Product.Description}"
            })
            .Where(x => targetSubstrateIds.Contains(x.SubstrateId))
            .OrderBy(x => x.ProductPosition)
            .GroupBy(x => x.FormattedProductDescription)
            .Select(g => new CalculatedAverageValuesDto(g.Key, targetSubstrateIds.ToDictionary(id => id,
                    id => Math.Round(g.Where(x => x.SubstrateId == id)
                                      .Average(x => (double?)x.UvColorMetricValue) ?? 0, 2))))
            ];
    }

    public static List<CalculatedAverageValuesDto> CalculateRadiationWavelengthAverages(IEnumerable<FrameMonitor> frameMonitors,
                                                                                        IEnumerable<long> targetSubstrateIds,
                                                                                        PropertyInfo propertyInfo)
    {
        return [.. frameMonitors
            .Where(fm => !Guard.Against.IsNull(fm.ProductSetup) && !Guard.Against.IsNull(fm.ProductSetup.Product))
            .SelectMany(fm => fm.FrameSubstrates, (fm, fs) => new { fm, fs })
            .SelectMany(temp => temp.fs.RadiationWavelengths, (temp, cm) => new
            {
                ProductPosition = temp.fm.ProductSetup.Position,
                ProductDescription = temp.fm.ProductSetup.Product.Description,
                temp.fs.SubstrateId,
                RadiationWavelengthValue = propertyInfo.GetValue(cm),
                FormattedProductDescription = $"{temp.fm.ProductSetup.Position.GetFormattedPosition()} - {temp.fm.ProductSetup.Product.Description}"
            })
            .Where(x => targetSubstrateIds.Contains(x.SubstrateId))
            .OrderBy(x => x.ProductPosition)
            .GroupBy(x => x.FormattedProductDescription)
            .Select(g => new CalculatedAverageValuesDto(g.Key, targetSubstrateIds.ToDictionary(id => id,
                    id => Math.Round(g.Where(x => x.SubstrateId == id)
                                      .Average(x => (double?)x.RadiationWavelengthValue) ?? 0, 2))))
            ];
    }

    public static int AddCalculationResult(this IXLWorksheet worksheet,
                                           WashCycleSetup washCycleSetup,
                                           List<CalculatedAverageValuesDto> results,
                                           int row,
                                           int column,
                                           List<long> targetSubstrateIds)
    {
        // Start writing matrix headers
        int matrixStartRow = row;
        int matrixStartColumn = column;
        for (int i = 0; i < targetSubstrateIds.Count; i++)
        {
            worksheet.Cell(matrixStartRow + 1, matrixStartColumn + 1 + i).Value = washCycleSetup.FrameMonitors.SelectMany(fm => fm.FrameSubstrates).Select(fs => fs.Substrate).FirstOrDefault(s => s.Id == targetSubstrateIds[i])?.Name
                                                                               ?? washCycleSetup.MonitorBatch.BatchFrameMonitors.SelectMany(fm => fm.BatchFrameSubstrates).Select(fs => fs.Substrate).FirstOrDefault(s => s.Id == targetSubstrateIds[i])?.Name;
            worksheet.Cell(matrixStartRow + 1, matrixStartColumn + 1 + i).Style.Fill.BackgroundColor = XLColor.LightCyan;
        }

        // Data rows: each product description and its averages
        for (int i = 0; i < results.Count; i++)
        {
            var product = results[i];
            worksheet.Cell(matrixStartRow + 2 + i, matrixStartColumn).Value = product.ProductDescription;
            worksheet.Cell(matrixStartRow + 2 + i, matrixStartColumn).Style.Fill.BackgroundColor = XLColor.LightBlue;

            for (int j = 0; j < targetSubstrateIds.Count; j++)
            {
                var substrateId = targetSubstrateIds[j];
                var avgValue = product.SubstrateAverages[substrateId];
                worksheet.Cell(matrixStartRow + 2 + i, matrixStartColumn + 1 + j).Value = avgValue;
            }
        }

        // Move row down after matrix
        return matrixStartRow + 2 + results.Count + 2; // add spacing
    }

    public static void AddExperimentDescriptionToWorksheet(this IXLWorksheet worksheet, ExperimentDto experiment, OrderDto order)
    {
        worksheet.Cell(1, 1).Value = "ExperimentID";
        worksheet.Cell(1, 2).Value = experiment.Id;

        worksheet.Cell(2, 1).Value = "CreatedBy";
        worksheet.Cell(2, 2).Value = experiment.CreatedBy;

        worksheet.Cell(3, 1).Value = "Experiment";
        worksheet.Cell(3, 2).Value = $"Id: {experiment.Id} | Type: {experiment.ExperimentType.Name} | Repetition: {experiment.NumberOfRepetition}";

        worksheet.Cell(4, 1).Value = "Order";
        worksheet.Cell(4, 2).Value = $"{order.ProductDeveloperAadGroup.Name} | {order.Description}";

        worksheet.Cell(5, 1).Value = "Experiment Description";
        worksheet.Cell(5, 2).Value = experiment.Description ?? "-";

        worksheet.Cell(6, 1).Value = "Experiment Comment";
        worksheet.Cell(6, 2).Value = experiment.Comment ?? "-";
    }
}
