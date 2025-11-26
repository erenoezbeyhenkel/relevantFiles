namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrdersByExperimentStatus;

public sealed record GetOrdersByExperimentStatusQueryResponse(IReadOnlyList<OrderDto> OrderDtos,
                                                              long TotalItems,
                                                              long TotalPageCount,
                                                              long NextCursor);