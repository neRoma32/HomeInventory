using MediatR;

namespace HomeInventory.Application.Features.Rooms.Commands.Delete;

public record DeleteRoomCommand : IRequest
{
    public Guid Id { get; init; }
}