using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.ExperimentStatus;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class ExperimentStatusMapperExtension
{
    public static ExperimentStatus ToEntity(this ExperimentStatusDto dto) => new() 
    {
        Id = dto.Id,
        Name = dto.Name
    };

    public static ExperimentStatusDto ToDto(this ExperimentStatus entity) => new(entity.Id,
                                                                                  entity.Name);
}
