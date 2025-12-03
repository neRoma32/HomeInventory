using MediatR;

namespace HomeInventory.Application.Features.Tags.Commands.Update;

public record UpdateTagCommand : IRequest
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}