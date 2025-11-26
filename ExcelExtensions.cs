using ClosedXML.Excel;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.WashCycleSetUp.Upload.ExternalExcelMeasurement;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.WashCycleSetUp.Upload.USRobotMeasurement;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions;

public static class ExcelExtensions
{
    public static List<USRobotMeasurementData> ReadUSRobotMeasurementExcelData(this byte[] excelFileBytes)
    {
        var result = new List<USRobotMeasurementData>();

        using var inputStream = new MemoryStream(excelFileBytes);
        using var workbook = new XLWorkbook(inputStream);
        var worksheet = workbook.Worksheets.First();

        int startRow = 4;
        while (true)
        {
            var row = worksheet.Row(startRow);
            if (row.Cell(1).IsEmpty())
                break;

            if (!int.TryParse(row.Cell(1).GetString(), out int washCycle)) break;
            if (!int.TryParse(row.Cell(2).GetString(), out int product)) break;
            if (!long.TryParse(row.Cell(5).GetString(), out long substrateId)) break;
            if (!int.TryParse(row.Cell(7).GetString(), out int run)) break;
            if (!int.TryParse(row.Cell(8).GetString(), out int rep)) break;
            if (!double.TryParse(row.Cell(9).GetString(), out  double L)) break;
            if (!double.TryParse(row.Cell(10).GetString(), out double A)) break;
            if (!double.TryParse(row.Cell(11).GetString(), out double B)) break;
            if (!double.TryParse(row.Cell(12).GetString(), out double dE)) break;
            if (!double.TryParse(row.Cell(13).GetString(), out double SRI)) break;

            result.Add(new USRobotMeasurementData(washCycle,
                                                  product,
                                                  substrateId,
                                                  run,
                                                  rep,
                                                  L,
                                                  A,
                                                  B,
                                                  dE,
                                                  SRI));

            startRow++;
        }

        return result;
    }

    public static List<ExternalExcelMeasurementData> ReadExternalExcelMeasurementData(this byte[] excelFileBytes)
    {
        var result = new List<ExternalExcelMeasurementData>();

        using var inputStream = new MemoryStream(excelFileBytes);
        using var workbook = new XLWorkbook(inputStream);
        var worksheet = workbook.Worksheets.First();

        int startRow = 2;
        while (true)
        {
            var row = worksheet.Row(startRow);
            if (row.Cell(1).IsEmpty())
                break;

            if (!int.TryParse(row.Cell(1).GetString(), out int washCycle)) break;

            string productStr = row.Cell(2).GetString().Trim();
            if (string.IsNullOrEmpty(productStr) || !char.IsLetter(productStr[0])) break;

            char productChar = char.ToUpper(productStr[0]);
            int product = productChar - 'A' + 1;


            if (!int.TryParse(row.Cell(3).GetString(), out int repetition)) break;
            if (!int.TryParse(row.Cell(4).GetString(), out int substrateId)) break;
            if (!double.TryParse(row.Cell(6).GetString(), out double L)) break;
            if (!double.TryParse(row.Cell(7).GetString(), out double A)) break;
            if (!double.TryParse(row.Cell(8).GetString(), out double B)) break;
            if (!double.TryParse(row.Cell(9).GetString(), out double X)) break;
            if (!double.TryParse(row.Cell(10).GetString(), out double Y)) break;
            if (!double.TryParse(row.Cell(11).GetString(), out double Z)) break;

            result.Add(new ExternalExcelMeasurementData(washCycle,
                                                        product,
                                                        repetition,
                                                        substrateId,
                                                        L,
                                                        A,
                                                        B,
                                                        X,
                                                        Y,
                                                        Z));

            startRow++;
        }

        return result;
    }
}
