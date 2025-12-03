using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using HomeInventory.Domain.Entities;
using MediatR;

namespace HomeInventory.Application.Features.Rooms.Commands.Create;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IRoomQueries _roomQueries;

    public CreateRoomCommandHandler(IApplicationDbContext context, IRoomQueries roomQueries)
    {
        _context = context;
        _roomQueries = roomQueries;
    }

    public async Task<RoomDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = Room.New(
            id: Guid.NewGuid(),
            name: request.Name);

        await _context.Rooms.AddAsync(room, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        var roomDto = await _roomQueries.GetByIdAsync(room.Id, cancellationToken);

        return roomDto!;
    }
}