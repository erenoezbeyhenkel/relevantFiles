using ErrorOr;
using Hcb.Rnd.Pwn.Common.Dto.Interfaces.Commands;
using MediatR;

namespace Hcb.Rnd.Pwn.Application.Interfaces.Messaging;

/// <summary>
/// https://www.youtube.com/watch?v=vdi-p9StmG0&ab_channel=MilanJovanovi%C4%87
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// The reason of implementing ICommandHandler to separate Command and Query Handlers and also increase the 
/// coding logic.
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, ErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
