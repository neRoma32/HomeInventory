using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Items.Queries.GetByRoom;

public class GetItemsByRoomIdQueryHandler : IRequestHandler<GetItemsByRoomIdQuery, IReadOnlyList<ItemDto>>
{
    private readonly IItemQueries _itemQueries;

    public GetItemsByRoomIdQueryHandler(IItemQueries itemQueries)
    {
        _itemQueries = itemQueries;
    }

    public async Task<IReadOnlyList<ItemDto>> Handle(
        GetItemsByRoomIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _itemQueries.GetByRoomIdAsync(request.RoomId, cancellationToken);
    }
}