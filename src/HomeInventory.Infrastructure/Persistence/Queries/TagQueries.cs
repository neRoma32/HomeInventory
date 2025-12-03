using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using HomeInventory.Domain.Entities;
using HomeInventory.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HomeInventory.Infrastructure.Persistence.Queries;

public class TagQueries : ITagQueries
{
    private readonly ApplicationDbContext _context;

    public TagQueries(ApplicationDbContext context)
    {
        _context = context;
    }

    private static readonly Expression<Func<Tag, TagDto>> s_mapToDto =
        t => new TagDto
        {
            Id = t.Id,
            Name = t.Name,
            CreatedAt = t.CreatedAt
        };

    public async Task<IReadOnlyList<TagDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Tags
            .AsNoTracking()
            .Select(s_mapToDto)
            .ToListAsync(cancellationToken);
    }

    public async Task<TagDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Tags
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(s_mapToDto)
            .FirstOrDefaultAsync(cancellationToken);
    }
}