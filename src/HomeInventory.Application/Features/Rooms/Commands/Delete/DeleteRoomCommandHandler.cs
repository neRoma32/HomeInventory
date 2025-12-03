using HomeInventory.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Application.Features.Rooms.Commands.Delete;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteRoomCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var hasItems = await _context.Items
            .AnyAsync(i => i.RoomId == request.Id, cancellationToken);

        if (hasItems)
        {

            throw new InvalidOperationException("Cannot delete room with items.");
        }

        var room = await _context.Rooms
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (room is not null)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}