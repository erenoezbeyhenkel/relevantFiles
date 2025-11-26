using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.AzureGroups;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Experiments;
using Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Products;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders;
using Hcb.Rnd.Pwn.Common.Extensions;
using Hcb.Rnd.Pwn.Domain.Entities.Orders;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions.EntityMappers.Orders;

public static class OrderMapperExtension
{
    public static Order ToEntity(this OrderDto dto) => new()
    {
        Id = dto.Id,
        Description = dto.Description,
        HugoProjectId = dto.HugoProjectId,
        InternalId = dto.InternalId,
        CreatedAt = dto.CreatedAt,
        ProductDeveloperAadGroup = dto.ProductDeveloperAadGroup?.ToEntity(),
        Products = dto.Products?.Select(p => p.ToEntity()).ToList()
    };
    public static OrderDto ToDto(this Order entity, IEnumerable<long> allowedExperimentIds) => new(entity.Id,
                                                                                                   entity.Description,
                                                                                                   entity.HugoProjectId,
                                                                                                   entity.InternalId,
                                                                                                   default,
                                                                                                   entity.CreatedAt,
                                                                                                   entity.ProductDeveloperAadGroup.ToDto(),
                                                                                                   entity.Experiments?.Where(ex => allowedExperimentIds.Contains(ex.Id))?.Select(e => e.ToExperimentSummaryDto())?.ToList() ?? [],
                                                                                                   entity.Products?.Select(p => p.ToDto()).ToList());
    public static OrderDto ToDto(this Order entity) => new(entity.Id,
                                                           entity.Description,
                                                           entity.HugoProjectId,
                                                           entity.InternalId,
                                                           default,
                                                           entity.CreatedAt,
                                                           entity.ProductDeveloperAadGroup.ToDto(),
                                                           entity.Experiments?.Select(e => e.ToExperimentSummaryDto()).ToList(),
                                                           entity.Products?.Select(p => p.ToDto()).ToList());

    public static OrderSummaryDto ToOrderSummaryDto(this Order entity) => new(entity.Id,
                                                                               entity.InternalId,
                                                                               entity.HugoProjectId,
                                                                               entity.Description,
                                                                               entity.Products.Select(p => $"{p.HugoWorksheetId} - {p.HugoProductId} - {p.HugoBatchId} - {p.HugoSampleId} - {p.HugoProductDescription}").ToList());

    public static void Map(this OrderDto dto, Order entity)
    {
        entity.Description = dto.Description;
        entity.HugoProjectId = dto.HugoProjectId;
        entity.InternalId = dto.InternalId;
        entity.ProductDeveloperAadGroupId = !Guard.Against.IsNull(dto.ProductDeveloperAadGroup) ? dto.ProductDeveloperAadGroup.Id : entity.ProductDeveloperAadGroupId;
        foreach (var dtoProduct in dto.Products)
        {
            var entityProduct = entity.Products.FirstOrDefault(x => x.Id == dtoProduct.Id);
            if (!Guard.Against.IsNull(dtoProduct) && !Guard.Against.IsNull(entityProduct))
                dtoProduct.Map(entityProduct);
        }
    }
}
