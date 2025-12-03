using MediatR;

namespace HomeInventory.Application.Features.Tags.Commands.Delete;

public record DeleteTagCommand : IRequest
{
    public Guid Id { get; init; }
}