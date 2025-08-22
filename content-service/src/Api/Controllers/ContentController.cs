using Application.Features.Commands.Content;
using Application.Features.Queries.Content;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/contents")]
    [ApiController]
    public class ContentController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ContentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContentAsync(CreateContentCommand command) {
            var response = await _mediator.Send(command);
            return StatusCode((int)response.Status, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContentAsync([FromRoute] Guid Id, [FromBody] UpdateContentCommand command)
        {
            command.Id = Id;
            var response = await _mediator.Send(command);
            return StatusCode((int)response.Status, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContentAsync([FromRoute] Guid Id)
        {
            var response = await _mediator.Send(new DeleteContentCommand { Id = Id });
            return StatusCode((int)response.Status, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContentAsync([FromRoute] Guid Id)
        {
            var response = await _mediator.Send(new GetContentQuery { Id = Id });
            return StatusCode((int)response.Status, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetContentsAsync([FromQuery] GetContentsQuery query)
        {
            var response = await _mediator.Send(query);
            return StatusCode((int)response.Status, response);
        }
    }
}
