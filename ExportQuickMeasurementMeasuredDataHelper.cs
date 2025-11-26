using ClosedXML.Excel;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.DataExports.MeasuredData.Export.QuickMeasurement;

namespace Hcb.Rnd.Pwn.Application.Common.Helpers;
public static class ExportQuickMeasurementMeasuredDataHelper
{
    public static void AddMeasuredDataToWorksheet(this IXLWorksheet worksheet, List<ExportQuickMeasurementMeasuredDataDto> exportQuickMeasurementMeasuredDataDtos)
    {
        int currentRow = 1;

        // Extract unique visible column names (from all rows)
        var allVisibleNames = exportQuickMeasurementMeasuredDataDtos
            .SelectMany(x => x.DataColorCalculationResultDtos)
            .Where(d => d.IsVisible)
            .Select(d => d.Name)
            .Distinct()
            .ToList();

        // Header row
        worksheet.Cell(currentRow, 1).Value = "Id";
        for (int i = 0; i < allVisibleNames.Count; i++)
            worksheet.Cell(currentRow, i + 2).Value = allVisibleNames[i];

        currentRow++;

        // Data rows
        foreach (var item in exportQuickMeasurementMeasuredDataDtos ?? [])
        {
            worksheet.Cell(currentRow, 1).Value = item.Id;

            for (int i = 0; i < allVisibleNames.Count; i++)
            {
                var columnName = allVisibleNames[i];
                var valueDto = item.DataColorCalculationResultDtos
                    .FirstOrDefault(d => d.Name == columnName && d.IsVisible);

                worksheet.Cell(currentRow, i + 2).Value = valueDto?.Value ?? default;
            }

            currentRow++;
        }
    }
}
