using Application.InternalFeatures.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("internal/users")]
    [ApiController]
    public class InternalUserController : ControllerBase
    {

        private readonly IMediator _mediator;
        public InternalUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInternalUserAsync([FromRoute] Guid Id)
        {
            var response = await _mediator.Send(new GetInternalUserQuery { Id = Id });
            return StatusCode((int)response.Status, response);
        }
    }
}
