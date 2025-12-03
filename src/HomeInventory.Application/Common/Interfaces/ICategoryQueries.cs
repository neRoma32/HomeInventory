using HomeInventory.Application.DTOs;

namespace HomeInventory.Application.Common.Interfaces;

public interface ICategoryQueries
{
    Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken cancellationToken);
}