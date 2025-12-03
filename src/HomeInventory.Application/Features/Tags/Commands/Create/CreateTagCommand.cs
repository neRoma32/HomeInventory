using MediatR;

namespace HomeInventory.Application.Features.Tags.Commands.Create;

public record CreateTagCommand : IRequest<Guid>
{
    public required string Name { get; init; }
}