using Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Base;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Orders.Expand;

public sealed record ExpandOrderCommand(ExpandOrderDto ExpandOrderDto) : BaseCommand<ExpandOrderCommandResponse>;
