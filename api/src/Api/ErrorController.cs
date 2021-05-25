using Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace StockTransactionProcessing.Api.Controllers
{
    /// <summary>
    /// Provides generic error response in case of unknown excpetion
    /// </summary>
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Provides generic error response 
        /// </summary>
        /// <returns>Problem response</returns>
        [Route("error")]
        [ApiExplorerSettings(IgnoreApi=true)]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if(context.Error is ValidationException ex)
            {
                var details = new ValidationProblemDetails(ex.Errors)
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                };

                return BadRequest(details);
            }

            _logger.LogError(context.Error, $"(500) InternalServerError: {context.Error.Message}");

            return Problem(
                "An error occurred while processing your request. Please try again later.",
                null,
                StatusCodes.Status500InternalServerError,
                "An error occurred while processing your request.",
                "https://tools.ietf.org/html/rfc7231#section-6.6.1");
        }
    }
}
