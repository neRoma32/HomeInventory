using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Rooms.Queries.GetAll;

public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, IReadOnlyList<RoomDto>>
{
    private readonly IRoomQueries _roomQueries;

    public GetAllRoomsQueryHandler(IRoomQueries roomQueries)
    {
        _roomQueries = roomQueries;
    }

    public async Task<IReadOnlyList<RoomDto>> Handle(
        GetAllRoomsQuery request,
        CancellationToken cancellationToken)
    {
        return await _roomQueries.GetAllAsync(cancellationToken);
    }
}