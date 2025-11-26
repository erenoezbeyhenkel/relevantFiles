using ErrorOr;
using MediatR;

namespace Hcb.Rnd.Pwn.Common.Dto.Interfaces.Queries;

/// <summary>
/// https://www.youtube.com/watch?v=vdi-p9StmG0&ab_channel=MilanJovanovi%C4%87
///  Base type and interface of queries. Please extends new query from this.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
}
