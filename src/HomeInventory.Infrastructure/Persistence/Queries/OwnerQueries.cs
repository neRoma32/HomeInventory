using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using HomeInventory.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Infrastructure.Persistence.Queries;

public class OwnerQueries : IOwnerQueries
{
    private readonly ApplicationDbContext _context;

    public OwnerQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OwnerDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<Domain.Entities.Owner>()
            .AsNoTracking()
            .Where(o => o.Id == id)
            .Select(o => new OwnerDto { Id = o.Id, FullName = o.FullName, Email = o.Email })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<OwnerDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<Domain.Entities.Owner>()
            .AsNoTracking()
            .Select(o => new OwnerDto { Id = o.Id, FullName = o.FullName, Email = o.Email })
            .ToListAsync(cancellationToken);
    }
}