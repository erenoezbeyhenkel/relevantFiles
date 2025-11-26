using Hcb.Rnd.Pwn.Common.Enums;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.ColorValueCalculation.DataColor;
public interface IConfigurableDataColorCalculator
{
    ExperimentType ExperimentType { get; }
    DeviceType DeviceType { get; }
    public bool IsReference { get; }
    public bool IsQuickMeasurement { get; }
    public bool IsUvGanzCheck { get; }
}
