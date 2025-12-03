using HomeInventory.Api.DTOs;
using HomeInventory.Application.DTOs;
using HomeInventory.Application.Features.Categories.Commands.Create;
using HomeInventory.Application.Features.Categories.Commands.Delete;
using HomeInventory.Application.Features.Categories.Commands.Update;
using HomeInventory.Application.Features.Categories.Queries.GetAll;
using HomeInventory.Application.Features.Categories.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HomeInventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class CategoriesController : ControllerBase
{
    private readonly ISender _sender;

    public CategoriesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAll()
    {
        var query = new GetAllCategoriesQuery();
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryRequest request)
    {
        var command = new CreateCategoryCommand { Name = request.Name };
        var result = await _sender.Send(command);

        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var command = new UpdateCategoryCommand
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
        var command = new DeleteCategoryCommand { Id = id };

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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CategoryDto>> GetById(Guid id)
    {
        var query = new GetCategoryByIdQuery(id);
        var result = await _sender.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}