using HomeInventory.Api.DTOs;
using HomeInventory.Application.DTOs;
using HomeInventory.Application.Features.Rooms.Commands.Create;
using HomeInventory.Application.Features.Rooms.Commands.Delete;
using HomeInventory.Application.Features.Rooms.Commands.Update;
using HomeInventory.Application.Features.Rooms.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly ISender _sender; 

    public RoomsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RoomDto>>> GetAll()
    {
        var query = new GetAllRoomsQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RoomDto>> Create([FromBody] CreateRoomRequest request)
    {
        var command = new CreateRoomCommand { Name = request.Name };
        var result = await _sender.Send(command);

        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateRoomRequest request)
    {
        var command = new UpdateRoomCommand
        {
            Id = id,
            Name = request.Name
        };
        await _sender.Send(command);

        return NoContent();
    }


    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteRoomCommand { Id = id };

        try
        {
            await _sender.Send(command);
        }
        catch (InvalidOperationException ex)
        {

            return BadRequest(new { message = ex.Message });
        }

        return NoContent();
    }
}
