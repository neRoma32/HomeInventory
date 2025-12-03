using HomeInventory.Api.DTOs;
using HomeInventory.Application.DTOs;
using HomeInventory.Application.Features.Tags.Commands.Create;
using HomeInventory.Application.Features.Tags.Commands.Delete;
using HomeInventory.Application.Features.Tags.Commands.Update;
using HomeInventory.Application.Features.Tags.Queries.GetAll;
using HomeInventory.Application.Features.Tags.Queries.GetById;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly ISender _sender;

    public TagsController(ISender sender)
    {
        _sender = sender;
    }

    // GET: api/tags
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TagDto>>> GetAll()
    {
        var result = await _sender.Send(new GetAllTagsQuery());
        return Ok(result);
    }

    // POST: api/tags
    [HttpPost]
    public async Task<ActionResult<TagDto>> Create([FromBody] CreateTagRequest request)
    {
        var command = new CreateTagCommand { Name = request.Name };
        var tagId = await _sender.Send(command);

        return Ok(new { Id = tagId });
    }

    // PUT: api/tags/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateTagRequest request)
    {
        var command = new UpdateTagCommand { Id = id, Name = request.Name };
        await _sender.Send(command);
        return NoContent();
    }

    // DELETE: api/tags/{id}
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _sender.Send(new DeleteTagCommand { Id = id });
        return NoContent();
    }

    // GET: api/tags/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TagDto>> GetById(Guid id)
    {
        var query = new GetTagByIdQuery { Id = id };
        var result = await _sender.Send(query);

        if (result == null)
        {
            return NotFound($"Tag with ID {id} not found.");
        }

        return Ok(result);
    }

}