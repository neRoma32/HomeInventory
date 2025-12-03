using HomeInventory.Application.DTOs;

namespace HomeInventory.Application.Common.Interfaces;

public interface IOwnerQueries
{
    Task<OwnerDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<OwnerDto>> GetAllAsync(CancellationToken cancellationToken);
}