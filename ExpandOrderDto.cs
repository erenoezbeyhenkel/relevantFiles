using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Expand;

public sealed record ExpandOrderDto(OrderDto Order,
                                    long? ExperimentIdForProductsOrder);