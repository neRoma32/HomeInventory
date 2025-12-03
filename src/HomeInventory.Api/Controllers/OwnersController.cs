using HomeInventory.Api.DTOs;
using HomeInventory.Application.DTOs;
using HomeInventory.Application.Features.Owners.Commands.Create;
using HomeInventory.Application.Features.Owners.Commands.Update;
using HomeInventory.Application.Features.Owners.Commands.Delete;
using HomeInventory.Application.Features.Owners.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnersController : ControllerBase
{
    private readonly ISender _sender;

    public OwnersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OwnerDto>>> GetAll()
    {
        return Ok(await _sender.Send(new GetAllOwnersQuery()));
    }

    [HttpPost]
    public async Task<ActionResult<OwnerDto>> Create([FromBody] CreateOwnerRequest request)
    {
        var command = new CreateOwnerCommand
        {
            FullName = request.FullName,
            Email = request.Email
        };
        var result = await _sender.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateOwnerRequest request)
    {
        var command = new UpdateOwnerCommand
        {
            Id = id,
            FullName = request.FullName,
            Email = request.Email
        };
        await _sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _sender.Send(new DeleteOwnerCommand { Id = id });
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}