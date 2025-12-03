using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Categories.Commands.Create;

public record CreateCategoryCommand : IRequest<CategoryDto>
{
    public required string Name { get; init; }
}