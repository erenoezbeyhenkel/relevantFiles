namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders;

public sealed record OrderSummaryDto(long Id,
                                      string InternalId,
                                      string HugoProjectId,
                                      string Description,
                                      List<string> ProductSummary);
