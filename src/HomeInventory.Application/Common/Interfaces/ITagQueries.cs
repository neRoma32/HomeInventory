using HomeInventory.Application.DTOs;

namespace HomeInventory.Application.Common.Interfaces;

public interface ITagQueries
{
    Task<IReadOnlyList<TagDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<TagDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}