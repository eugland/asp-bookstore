using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controller;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("error")]
    public IActionResult HandleError()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error; // Your exception
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected

        if (exception?.Message == "404" ) code = HttpStatusCode.NotFound;
        else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
        // Add more custom exception handling here as needed

        Response.StatusCode = (int)code;

        return new JsonResult(new
        {
            error = new
            {
                message = exception?.Message,
                stackTrace = exception?.StackTrace
            }
        });
    }
}