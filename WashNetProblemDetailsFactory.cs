using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Hcb.Rnd.Pwn.Application.Common.Constants;
using System.Diagnostics;
using Hcb.Rnd.Pwn.Common.Extensions;

namespace Hcb.Rnd.Pwn.Api.Common.Errors;

/// <summary>
/// https://www.youtube.com/watch?v=gMwAhKddHYQ&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=4&ab_channel=AmichaiMantinband
/// Custom problem details factory needed to use ErrorOr and problems.
/// </summary>
public class WashNetProblemDetailsFactory(IOptions<ApiBehaviorOptions> options) : ProblemDetailsFactory
{
    public ApiBehaviorOptions Options = options?.Value ?? throw new ArgumentNullException(nameof(options));

    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string title = null,
        string type = null,
        string detail = null,
        string instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string title = null,
        string type = null,
        string detail = null,
        string instance = null)
    {
        if (Guard.Against.IsNull(modelStateDictionary))
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (title != null)
        {
            // For validation problem details, don't overwrite the default title with null.
            problemDetails.Title = title;
        }

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (Options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        //We set the operation id and log it to trace exception.
        var operationId = Activity.Current?.RootId;
        if (!Guard.Against.IsNull(operationId))
        {
            problemDetails.Extensions["operationId"] = operationId;
        }

        //We prepare the errors to show them to users
        var errors = httpContext?.Items[HttpContextItemKeys.Errors] as List<Error>;
        if (!Guard.Against.IsNull(errors))
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
        }
    }
}
