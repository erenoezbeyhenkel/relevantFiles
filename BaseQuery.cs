using Hcb.Rnd.Pwn.Common.Dto.Interfaces.Queries;

namespace Hcb.Rnd.Pwn.Common.Dto.Implementations.Queries.Base;

public record BaseQuery<TResponse>() : IQuery<TResponse>
{
}