using MediatR;

namespace HomeInventory.Application.Features.Categories.Commands.Update;

public record UpdateCategoryCommand : IRequest
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}