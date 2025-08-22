using Application.Features.Commands.User;
using Application.Features.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(CreateUserCommand command) {
            var response = await _mediator.Send(command);
            return StatusCode((int)response.Status, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid Id, [FromBody] UpdateUserCommand command)
        {
            command.Id = Id;
            var response = await _mediator.Send(command);
            return StatusCode((int)response.Status, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid Id)
        {
            var response = await _mediator.Send(new DeleteUserCommand { Id = Id });
            return StatusCode((int)response.Status, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync([FromRoute] Guid Id)
        {
            var response = await _mediator.Send(new GetUserQuery { Id = Id });
            return StatusCode((int)response.Status, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] GetUsersQuery query)
        {
            var response = await _mediator.Send(query);
            return StatusCode((int)response.Status, response);
        }
    }
}
