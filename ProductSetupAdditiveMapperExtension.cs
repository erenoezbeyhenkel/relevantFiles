using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.ProductSetup;
using Hcb.Rnd.Pwn.Domain.Entities.Experiments;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;

public static class ProductSetupAdditiveMapperExtension
{
    public static ProductSetupAdditive ToEntity(this ProductSetupAdditiveDto dto) => new()
    {
        Id = dto.Id,
        ProductSetupId = dto.ProductSetupId,
        AdditiveTypeId = dto.AdditiveTypeId,
        Amount = dto.Amount,
        IsProductDependent = dto.IsProductDependent,
    };

    public static ProductSetupAdditiveDto ToDto(this ProductSetupAdditive entity) => new(entity.Id,
                                                                                          entity.ProductSetupId,
                                                                                          entity.AdditiveTypeId,
                                                                                          $"{entity.AdditiveType?.Name}  [{entity.Amount} {entity.AdditiveType?.Unit}] {(entity.IsProductDependent ? "[P Dependent]" : string.Empty)}",
                                                                                          entity.AdditiveType.Unit,
                                                                                          entity.Amount,
                                                                                          entity.IsProductDependent);

    public static void Map(this ProductSetupAdditiveDto dto, ProductSetupAdditive entity)
    {
        entity.ProductSetupId = dto.ProductSetupId;
        entity.AdditiveTypeId = dto.AdditiveTypeId;
        entity.Amount = dto.Amount;
        entity.IsProductDependent = dto.IsProductDependent;
    }
}
