using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Measurement.ColorMetric;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Measurement.Mach5;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Monitors.MonitorSubstrate;
using Hcb.Rnd.Pwn.Common.Extensions;
using System.Globalization;
using System.Linq;

namespace Hcb.Rnd.Pwn.Common.Helpers;

public static class ColorMetricHelper
{
    public static async Task<(string errorMessage, List<SubstrateDefaultColorMetricDto> substrateDefaultColorMetricItems)> ValidateColorMetricData(List<MonitorSubstrateDto> monitorSubstrateDtos, List<BatchFrameSubstrateDto> batchFrameSubstrateDtos, byte[] data, bool isValidation)
    {
        await Task.CompletedTask;
        var errorMessage = string.Empty;
        var substrateDefaultColorMetricItems = new List<SubstrateDefaultColorMetricDto>();

        var mach5MeasurementDtos = PrepareColorMetricItems(data);

        if (mach5MeasurementDtos.Any(m => m.Y >= 97.0))
        {
            errorMessage = "The measured data contains a Y value greater than 97.0. This is not allowed! Please measure again. If this happens again please contact the MACH5 device support!, Mapping not possible!";
            return (errorMessage, substrateDefaultColorMetricItems);
        }

        var monitorSubstrates = monitorSubstrateDtos.Where(ms => !Guard.Against.IsNull(ms.Substrate)).OrderBy(ms => ms.Position);
        var maxSubstrateCount = mach5MeasurementDtos.Max(m => Convert.ToInt32(m.SubstratePosition));
        var substrateCountOfMonitor = monitorSubstrates.Count();
        if (maxSubstrateCount != substrateCountOfMonitor)
        {
            errorMessage = $"Mapping is not possible! The measured data file contains {maxSubstrateCount} substrates. But the current monitor have {substrateCountOfMonitor}. This measure data file cannot be mapped !";
            return (errorMessage, substrateDefaultColorMetricItems);
        }

        if (!isValidation)
        {
            foreach (var ms in monitorSubstrates)
            {
                var measuredDataOfSubstate = mach5MeasurementDtos.Where(cmi => cmi.SubstratePosition == ms.Position);
                var batchFrameSubstrate = batchFrameSubstrateDtos.FirstOrDefault(fs => fs.SubstrateId == ms.Substrate.Id);
                substrateDefaultColorMetricItems.AddRange(measuredDataOfSubstate.Select(mdos => new SubstrateDefaultColorMetricDto(batchFrameSubstrate.Id,
                                                                                                                                   mdos.MeasurementPoint,
                                                                                                                                   mdos.X,
                                                                                                                                   mdos.Y,
                                                                                                                                   mdos.Z,
                                                                                                                                   mdos.L,
                                                                                                                                   mdos.A,
                                                                                                                                   mdos.B)));
            }
        }

        return (errorMessage, substrateDefaultColorMetricItems);
    }

    public static List<Mach5MeasurementDto> PrepareColorMetricItems(byte[] data)
    {
        var mach5CurrentMeasurementItemsList = new List<Mach5MeasurementDto>();
        using (MemoryStream memoryStream = new(data))
        using (StreamReader reader = new(memoryStream))
        {
            var lines = reader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            lines.Skip(1).ToList().ForEach(line =>
            {
                var elements = line.Split('\t');
                mach5CurrentMeasurementItemsList.Add(new Mach5MeasurementDto
                    (Convert.ToInt32(elements[0]),
                     Convert.ToInt32(elements[1]),
                     default,
                     default,
                     Convert.ToDouble(elements[5]),
                     Convert.ToDouble(elements[6]),
                     Convert.ToDouble(elements[7]),
                     Convert.ToDouble(elements[2]),
                     Convert.ToDouble(elements[3]),
                     Convert.ToDouble(elements[4])));
            });
        }
        return mach5CurrentMeasurementItemsList;
    }

    public static decimal ToDecimal(this string value, decimal defaultValue)
    {
        var formatinfo = new NumberFormatInfo
        {
            NumberDecimalSeparator = "."
        };

        if (decimal.TryParse(value, NumberStyles.Float, formatinfo, out decimal d))
            return d;

        formatinfo.NumberDecimalSeparator = ",";

        if (decimal.TryParse(value, NumberStyles.Float, formatinfo, out d))
            return d;

        return defaultValue;
    }
}
