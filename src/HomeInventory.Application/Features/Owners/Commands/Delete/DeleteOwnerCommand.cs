using MediatR;

namespace HomeInventory.Application.Features.Owners.Commands.Delete;

public record DeleteOwnerCommand : IRequest
{
    public Guid Id { get; init; }
}