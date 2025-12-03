using MediatR;

namespace HomeInventory.Application.Features.Categories.Commands.Delete;

public record DeleteCategoryCommand : IRequest
{
    public Guid Id { get; init; }
}