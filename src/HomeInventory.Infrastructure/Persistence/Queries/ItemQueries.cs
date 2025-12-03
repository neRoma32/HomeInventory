using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using HomeInventory.Domain.Entities;
using HomeInventory.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using HomeInventory.Application.Common.Models; 

namespace HomeInventory.Infrastructure.Persistence.Queries;

public class ItemQueries : IItemQueries
{
    private readonly ApplicationDbContext _context;

    public ItemQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    private static readonly Expression<Func<Item, ItemDto>> s_mapToDto =
        i => new ItemDto
        {
            Id = i.Id,
            Name = i.Name,
            Description = i.Description,
            Status = i.Status.ToString(),
            RoomId = i.RoomId,
            CategoryId = i.CategoryId,
            CreatedAt = i.CreatedAt,
            RoomName = i.Room.Name,
            CategoryName = i.Category.Name,
            OwnerName = i.Owner.FullName,

            HasWarranty = i.Warranty != null,
            WarrantyProvider = i.Warranty != null ? i.Warranty.Provider : null,
            WarrantyExpiration = i.Warranty != null ? i.Warranty.ExpirationDate : null,

            Tags = i.Tags.Select(t => t.Name).ToList()
        };

    public async Task<Optional<ItemDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await _context.Items
            .AsNoTracking()
            .Include(i => i.Room)
            .Include(i => i.Category)
            .Include(i => i.Owner)
            .Include(i => i.Warranty)
            .Include(i => i.Tags)
            .Where(i => i.Id == id)
            .Select(s_mapToDto)
            .FirstOrDefaultAsync(cancellationToken);

        return item is not null
            ? Optional<ItemDto>.Some(item)
            : Optional<ItemDto>.None();
    }

    public async Task<IReadOnlyList<ItemDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Items
            .AsNoTracking()
            .Include(i => i.Room)
            .Include(i => i.Category)
            .Include(i => i.Owner)
            .Include(i => i.Warranty)
            .Include(i => i.Tags)
            .Select(s_mapToDto)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ItemDto>> GetByRoomIdAsync(Guid roomId, CancellationToken cancellationToken)
    {
        return await _context.Items
            .AsNoTracking()
            .Include(i => i.Room)
            .Include(i => i.Category)
            .Include(i => i.Owner)
            .Include(i => i.Warranty)
            .Include(i => i.Tags)
            .Where(i => i.RoomId == roomId)
            .Select(s_mapToDto)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ItemDto>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await _context.Items
            .AsNoTracking()
            .Include(i => i.Room)
            .Include(i => i.Category)
            .Include(i => i.Owner)
            .Include(i => i.Warranty)
            .Include(i => i.Tags)
            .Where(i => i.CategoryId == categoryId)
            .Select(s_mapToDto)
            .ToListAsync(cancellationToken);
    }
}