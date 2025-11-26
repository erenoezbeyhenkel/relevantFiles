using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Base;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders.GetOrderById;

public sealed record GetOrderByIdQuery(long OrderId) : BaseQuery<GetOrderByIdQueryResponse>;