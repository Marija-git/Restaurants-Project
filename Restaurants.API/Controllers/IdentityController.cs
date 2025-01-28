using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands;

namespace Restaurants.API.Controllers
{  
    [ApiController]
    [Route("api/identity")]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}
