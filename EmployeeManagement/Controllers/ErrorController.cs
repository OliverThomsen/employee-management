using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        public ILogger<ErrorController> Logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            Logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry the page could not be found";
                    Logger.LogWarning($"404 Error Occoured. Path: {statusCodeResult.OriginalPath}, QueryString: {statusCodeResult.OriginalQueryString}");
                    break;
            }

            return View("NotFound");
        }

        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            // Exception details
            var exceptionDetails =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var path = exceptionDetails.Path;
            var errorMessage = exceptionDetails.Error.Message;
            var stackTrace = exceptionDetails.Error.StackTrace;

            Logger.LogError($"{errorMessage} {path} {stackTrace}");

            return View("Error");
        }
    }
}
