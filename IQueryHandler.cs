using ErrorOr;
using Hcb.Rnd.Pwn.Common.Dto.Interfaces.Queries;
using MediatR;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Messaging;

/// <summary>
/// https://www.youtube.com/watch?v=vdi-p9StmG0&ab_channel=MilanJovanovi%C4%87
/// </summary>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// The reason of implementing IQueryHandler to separate Command and Query Handlers and also increase the 
/// coding logic.
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, ErrorOr<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
