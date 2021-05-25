using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api.V1.Controllers
{
    /// <summary>
    /// Base controller, used for setting common attributes and Mediator instance
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private IMediator _mediator;

        /// <summary>
        /// Mediator instanse
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
