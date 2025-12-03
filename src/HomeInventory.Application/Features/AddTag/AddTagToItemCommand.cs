using MediatR;

namespace HomeInventory.Application.Features.Items.Commands.AddTag;

public record AddTagToItemCommand : IRequest
{
    public Guid ItemId { get; init; }
    public Guid TagId { get; init; }
}