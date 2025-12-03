using MediatR;

namespace HomeInventory.Application.Features.Rooms.Commands.Update;

public record UpdateRoomCommand : IRequest
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}