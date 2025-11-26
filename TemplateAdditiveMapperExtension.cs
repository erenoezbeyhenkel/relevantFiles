using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.TemplateAdditive;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class TemplateAdditiveMapperExtension
{
    public static TemplateAdditive ToEntity(this TemplateAdditiveDto dto) => new()
    {
        Id = dto.Id,
        ExperimentId = dto.ExperimentId,
        AdditiveTypeId = dto.AdditiveTypeId,
        Amount = dto.Amount,
        IsProductDependent = dto.IsProductDependent
    };

    public static TemplateAdditiveDto ToDto(this TemplateAdditive entity) => new(entity.Id,
                                                                                  entity.ExperimentId,
                                                                                  entity.AdditiveTypeId,
                                                                                  !Guard.Against.IsNull(entity.AdditiveType.Category) 
                                                                                  ? entity.AdditiveType.Category.Trim().ToUpper().Equals("DTI", StringComparison.OrdinalIgnoreCase) ? $"{entity.AdditiveType?.Name} [Product Dependent]" : entity.AdditiveType?.Name
                                                                                  : default,                                                      
                                                                                  entity.AdditiveType?.Unit,
                                                                                  entity.Amount,
                                                                                  entity.IsProductDependent);

    public static void Map(this TemplateAdditiveDto dto, TemplateAdditive entity)
    {
        entity.ExperimentId = dto.ExperimentId;
        entity.AdditiveTypeId = dto.AdditiveTypeId;
        entity.Amount = dto.Amount;
        entity.IsProductDependent = dto.IsProductDependent;
    }
}
