using Hcb.Rnd.Pwn.Common.Enums;
using Hcb.Rnd.Pwn.Common.Extensions;

namespace Hcb.Rnd.Pwn.Application.Common.Helpers;
public sealed class Mach5CalculationScenarioKey(ExperimentType experimentType, DeviceType deviceType, bool isReference)
{
    public ExperimentType ExperimentType { get; } = experimentType;
    public DeviceType DeviceType { get; } = deviceType;
    public bool IsReference { get; } = isReference;

    public override bool Equals(object obj)
    {
        if (Guard.Against.IsNull(obj))
            return false;

        return obj is Mach5CalculationScenarioKey key &&
               ExperimentType == key.ExperimentType &&
               DeviceType == key.DeviceType &&
               IsReference == key.IsReference;
    }

    public override int GetHashCode() => HashCode.Combine(ExperimentType, DeviceType, IsReference);
}

