using Application.Photos.Queries;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Application.Common.Models;

namespace Api.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/")]
    public class PhotosController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PaginatedList<Photo>>> Get([FromQuery] GetPhotosQuery photosQuery)
        {
            return await Mediator.Send(photosQuery);
        }
    }
}
