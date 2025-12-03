using HomeInventory.Api.DTOs;
using HomeInventory.Application.Common.Models; 
using HomeInventory.Application.DTOs;
using HomeInventory.Application.Features.Items.Commands.AddTag;
using HomeInventory.Application.Features.Items.Commands.AddWarranty;
using HomeInventory.Application.Features.Items.Commands.Create;
using HomeInventory.Application.Features.Items.Commands.Delete;
using HomeInventory.Application.Features.Items.Commands.Update;
using HomeInventory.Application.Features.Items.Queries.GetAll;
using HomeInventory.Application.Features.Items.Queries.GetById;
using HomeInventory.Application.Features.Items.Queries.GetByRoom;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ISender _sender;

    public ItemsController(ISender sender)
    {
        _sender = sender;
    }

    // POST /api/items
    [HttpPost]
    public async Task<ActionResult<ItemDto>> Create([FromBody] CreateItemRequest request)
    {
        var command = new CreateItemCommand
        {
            Name = request.Name,
            Description = request.Description,
            RoomId = request.RoomId,
            CategoryId = request.CategoryId,
            OwnerId = request.OwnerId,
        };

        var result = await _sender.Send(command);

        return result.Match<ActionResult<ItemDto>>(
            onSuccess: id => CreatedAtAction(nameof(GetById), new { id }, new { id }),
            onFailure: error => BadRequest(new { error.Code, error.Description })
        );
    }

    // UPDATE
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateItemRequest request)
    {
        var command = new UpdateItemCommand { Id = id, Name = request.Name, Description = request.Description };
        var result = await _sender.Send(command);

        return result.Match<ActionResult>(
            onSuccess: _ => NoContent(),
            onFailure: error => error == Error.None
                ? NotFound() 
                : BadRequest(error)
        );
    }

    // GET /api/items
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ItemDto>>> GetAll()
    {
        var query = new GetAllItemsQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }

    // GET /api/items/by-room/{roomId:guid}
    [HttpGet("by-room/{roomId:guid}")]
    public async Task<ActionResult<IReadOnlyList<ItemDto>>> GetByRoom(Guid roomId)
    {
        var query = new GetItemsByRoomIdQuery { RoomId = roomId };
        var result = await _sender.Send(query);
        return Ok(result);
    }

    // DELETE
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteItemCommand { Id = id };
        var result = await _sender.Send(command);

        return result.Match<ActionResult>(
            onSuccess: _ => NoContent(),
            onFailure: error => NotFound()
        );
    }

    // POST /api/items/{id}/warranty
    [HttpPost("{id:guid}/warranty")]
    public async Task<ActionResult<Guid>> AddWarranty(Guid id, [FromBody] CreateWarrantyRequest request)
    {
        var command = new AddWarrantyCommand
        {
            ItemId = id,
            Provider = request.Provider,
            SupportContact = request.SupportContact,
            ExpirationDate = request.ExpirationDate
        };

        var result = await _sender.Send(command);

        return result.Match<ActionResult<Guid>>(
            onSuccess: warrantyId => Ok(warrantyId),
            onFailure: error => error.Code == "Error.NotFound"
                ? NotFound(new { error.Code, error.Description })
                : BadRequest(new { error.Code, error.Description })
        );
    }

    // POST: api/items/{id}/tags
    [HttpPost("{id:guid}/tags")]
    public async Task<ActionResult> AddTagToItem(Guid id, [FromBody] AddTagToItemRequest request)
    {
        var command = new AddTagToItemCommand
        {
            ItemId = id,
            TagId = request.TagId
        };

        await _sender.Send(command);

        return NoContent();
    }

    // GET /api/items/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemDto>> GetById(Guid id)
    {
        var query = new GetItemByIdQuery(id);
        var result = await _sender.Send(query);

        return result.Match<ActionResult<ItemDto>>(
            onSome: dto => Ok(dto),
            onNone: () => NotFound()
        );
    }
}