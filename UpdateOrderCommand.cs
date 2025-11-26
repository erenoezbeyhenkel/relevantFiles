using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Base;
using Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Orders;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Update;

public sealed record UpdateOrderCommand(OrderDto Order) : BaseCommand<UpdateOrderCommandResponse>;