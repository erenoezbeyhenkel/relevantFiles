using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Hcb.Rnd.Pwn.Api.Controllers.Base;

/// <summary>
/// Base error controller.
/// </summary>
public sealed class ErrorsController : ControllerBase
{
    /// <summary>
    /// default controller.
    /// </summary>
    /// <returns></returns>
    [Route("/error")]
    protected IActionResult Error()
    {
        Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem();
    }
}
