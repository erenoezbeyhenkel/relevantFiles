using Hcb.Rnd.Pwn.Common.Enums;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.ColorValueCalculation.DataColor;
public interface IDataColorCalculatorFactory
{
    IDataColorCalculator GetCalculator(ExperimentType experimentType, DeviceType deviceType, bool isReference, bool isQuickMeasurement, bool isUvGanzCheck);
}

