using ErrorOr;
using Hcb.Rnd.Pwn.Application.Common.Extensions;
using Hcb.Rnd.Pwn.Application.Interfaces.Authentication;
using Hcb.Rnd.Pwn.Common.Extensions;
using MediatR;
using Serilog.Context;
using SerilogTimings;
using GeneralExtensions = Hcb.Rnd.Pwn.Application.Common.Extensions.GeneralExtensions;

namespace Hcb.Rnd.Pwn.Application.Common.Behaviors;

/// <summary>
/// https://www.youtube.com/watch?v=JVX9MMpO6pE&ab_channel=MilanJovanovi%C4%87
/// https://www.milanjovanovic.tech/blog/5-serilog-best-practices-for-better-structured-logging
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
/// <param name="logger"></param>
/// <param name="dateTimeProvider"></param>
/// <param name="pwnHttpContextAccessor"></param>
public sealed class LoggingBehavior<TRequest, TResponse>(IPwnHttpContextAccessor pwnHttpContextAccessor) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //Calculate the execution time of the business logic and log it.
        //Once the system hit this line to log "Operation.Time, all our custom properties in the log context eill be logged into the AppInsights.
        //u means : user, rt means: request type. o means : operation.
        using (Operation.Time($"#u: {pwnHttpContextAccessor.UserEmail} - #rt: {typeof(TRequest).Name} - #o :"))
        {
            if (!Guard.Against.IsNull(pwnHttpContextAccessor.HttpContext))
            {
                string correlationId = GeneralExtensions.GetCorrelationId(pwnHttpContextAccessor.HttpContext);
                LogContext.PushProperty("CorrelationId", correlationId);
            }

            LogContext.PushProperty("UserEmail", pwnHttpContextAccessor.UserEmail);

            //Check if the request is query. IF query just log the request info.
            if (request.IsQuery())
            {
                LogContext.PushProperty("RequestContent", request);

                //Invoke next behavior or middleware.
                return await next(cancellationToken);
            }

            LogContext.PushProperty("RequestContent", request);

            //Invoke next behavior or middleware.
            var result = await next(cancellationToken);

            LogContext.PushProperty(request.IsCommand() ? "CommandResponse" : request.IsNotification() ? "NotificationResponse" : "UndefinedResponseType", result);

            //If there is any error returns then log it as Error.
            if (result.IsError)
                LogContext.PushProperty(request.IsCommand() ? "CommandResponseErrors" : request.IsNotification() ? "NotificationResponseErrors" : "UndefinedResponseTypeErros", result.Errors);

            return result;
        }
    }
}
