using System.Threading.Tasks;
using Api.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoggedUserVM>> Login([FromBody] AuthenticateVM model)
        {
            LoggedUserVM result = await _mediator.Send(model);
            return Ok(result);
        }
    }
}
