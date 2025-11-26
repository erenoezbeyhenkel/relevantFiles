using Hcb.Rnd.Pwn.Common.Dto.Interfaces.Commands;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Commands.Base;

public record BaseCommand<TResponse>() : ICommand<TResponse>
{
}
