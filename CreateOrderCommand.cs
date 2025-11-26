using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Base;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Create;

public sealed record CreateOrderCommand(OrderDto Order) : BaseCommand<CreateOrderCommandResponse>;