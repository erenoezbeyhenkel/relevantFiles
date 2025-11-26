using ErrorOr;
using MediatR;

namespace Hcb.Rnd.Pwn.Common.Dto.Interfaces.Commands;

/// <summary>
/// https://www.youtube.com/watch?v=vdi-p9StmG0&ab_channel=MilanJovanovi%C4%87
/// Base type and interface of commands. Please extends new command from this.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
}
