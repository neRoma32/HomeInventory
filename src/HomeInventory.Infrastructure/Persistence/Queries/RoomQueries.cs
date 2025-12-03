using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using HomeInventory.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Infrastructure.Persistence.Queries;

public class RoomQueries : IRoomQueries
{
    private readonly ApplicationDbContext _context;

    public RoomQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RoomDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Rooms
            .AsNoTracking() 
            .Where(r => r.Id == id)
            .Select(r => new RoomDto
            {
                Id = r.Id,
                Name = r.Name
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<RoomDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Rooms
            .AsNoTracking()
            .Select(r => new RoomDto
            {
                Id = r.Id,
                Name = r.Name
            })
            .ToListAsync(cancellationToken);
    }
}