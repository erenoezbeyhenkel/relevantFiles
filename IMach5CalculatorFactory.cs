using Hcb.Rnd.Pwn.Common.Enums;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Services.ColorValueCalculation.Mach5;
public interface IMach5CalculatorFactory
{
    IMach5Calculator GetCalculator(ExperimentType experimentType, DeviceType deviceType, bool isReference);
}
