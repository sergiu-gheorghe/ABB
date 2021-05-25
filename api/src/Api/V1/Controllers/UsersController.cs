using Application.Users.Queries;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]/")]
    public class UsersController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<User>>> Get([FromQuery]GetUsersQuery query)
        {
            var result = await Mediator.Send(query);
            return result.ToList();
        }
    }
}
