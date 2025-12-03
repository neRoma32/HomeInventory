using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Rooms.Commands.Create;

public record CreateRoomCommand : IRequest<RoomDto>
{
    public required string Name { get; init; }
}