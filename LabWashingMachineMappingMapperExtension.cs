using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.LabWashingMachineMapping;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;
public static class LabWashingMachineMappingMapperExtension
{
    public static LabWashingMachineMappingDto ToDto(this LabWashingMachineMapping labWashingMachineMapping) => new(labWashingMachineMapping.LabWashingMachineId,
                                                                                                                   labWashingMachineMapping.WashingMachineId);
}
