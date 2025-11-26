using ClosedXML.Excel;
using Hcb.Rnd.Pwn.Application.Common.Constants;
using Hcb.Rnd.Pwn.Common.Dto.Interfaces.Commands;
using Hcb.Rnd.Pwn.Common.Dto.Interfaces.Queries;
using Hcb.Rnd.Pwn.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions;

public static class GeneralExtensions
{
    /// <summary>
    /// Returns if the object extends ICommand
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static bool IsCommand(this object command) => command.GetType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommand<>));

    /// <summary>
    /// Returns if the object extends IQuery
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static bool IsQuery(this object command) => command.GetType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IQuery<>));

    /// <summary>
    /// Returns is the object extend INotification
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    public static bool IsNotification(this object notification) => notification.GetType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(INotification));

    public static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(GeneralConstants.CorrelationIdHeaderName, out StringValues correlationId);

        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }

    public static void CreateError(this bool showTicket, bool isDev)
    {
        if (isDev && showTicket)
            throw new NullReferenceException("Null reference exception to test azure devops creation process. If you want to remove this, go to Azure App Configuration and set Pwn:DevOpsOptions:ShowDemoTicket to False.");
    }

    public static bool IsRowTrulyEmpty(this IXLRow row)
    {
        foreach (var cell in row.CellsUsed())
        {
            if (!Guard.Against.IsNullOrEmpty(cell.GetValue<string>()))
                return false;
        }

        return true;
    }
}
