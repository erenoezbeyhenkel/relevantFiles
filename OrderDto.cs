using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.AzureGroups.GetAllProductDeveloperAadGroups;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Experiments.Experiment;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Products.Product;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders;

public sealed record OrderDto(long Id,
                              string Description,
                              string HugoProjectId,
                              string? InternalId,
                              long ExperimentTemplateId,
                              DateTime CreatedAt,
                              ProductDeveloperAadGroupDto ProductDeveloperAadGroup,
                              List<ExperimentSummaryDto> ExperimentSummaries,
                              IReadOnlyList<ProductDto> Products);
