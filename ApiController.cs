using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Hcb.Rnd.Pwn.Application.Common.Constants;


namespace Hcb.Rnd.Pwn.Api.Controllers.Base;

/// <summary>
/// Api controller is base. We have to extend all our controllers from this and pass ISender as a parameter. ISender comes from MediatR to send the request to respective command or query handler.
/// All endpoints marked Authorized here. 
/// </summary>
/// <param name="sender"></param>
[Authorize]
[ApiController]
public class ApiController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Sender of MediatR
    /// </summary>
    protected ISender Sender { get; } = sender;


    /// <summary>
    /// https://www.youtube.com/watch?v=gMwAhKddHYQ&list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&index=4&ab_channel=AmichaiMantinband
    /// It prepares the Problem depends pn the ErrorType.
    /// </summary>
    /// <param name="errors"></param>
    /// <returns></returns>
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count <= 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        return Problem(errors[0]);
    }

    /// <summary>
    /// Here we find the error codes and set up the problem
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    private ObjectResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private ActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors ?? Enumerable.Empty<Error>())
        {
            modelStateDictionary.AddModelError(error.Code, error.Description);
        }
        return ValidationProblem(modelStateDictionary);
    }
}
