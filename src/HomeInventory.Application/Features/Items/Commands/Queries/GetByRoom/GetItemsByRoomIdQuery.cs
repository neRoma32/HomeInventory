using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Items.Queries.GetByRoom;

public record GetItemsByRoomIdQuery : IRequest<IReadOnlyList<ItemDto>>
{
    public Guid RoomId { get; init; }
}