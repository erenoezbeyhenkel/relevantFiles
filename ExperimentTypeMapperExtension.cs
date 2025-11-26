using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.ExperimentType;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class ExperimentTypeMapperExtension
{
    public static ExperimentType ToEntity(this ExperimentTypeDto dto) => new()
    {
        Id = dto.Id,
        Name = dto.Name,
        WorkInstructionPath = dto.WorkInstructionPath,
    };

    public static ExperimentTypeDto ToDto(this ExperimentType entity) => new(entity.Id,
                                                                               entity.Name,
                                                                               entity.WorkInstructionPath);
}
