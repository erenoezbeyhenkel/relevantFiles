using Hcb.Rnd.Pwn.Common.Enums;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.ColorValueCalculation.Mach5;
public interface IConfigurableMach5Calculator
{
    ExperimentType ExperimentType { get; }
    DeviceType DeviceType { get; }
    public bool IsReference { get; }
}
