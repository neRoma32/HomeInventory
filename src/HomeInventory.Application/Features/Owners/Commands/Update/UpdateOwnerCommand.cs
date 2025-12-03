using MediatR;

namespace HomeInventory.Application.Features.Owners.Commands.Update;

public record UpdateOwnerCommand : IRequest
{
    public Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
}