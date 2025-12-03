using HomeInventory.Application.DTOs;
using HomeInventory.Application.Common.Models;

namespace HomeInventory.Application.Common.Interfaces;

public interface IItemQueries
{
    Task<Optional<ItemDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ItemDto>> GetAllAsync(CancellationToken cancellationToken);

    Task<IReadOnlyList<ItemDto>> GetByRoomIdAsync(Guid roomId, CancellationToken cancellationToken);

    Task<IReadOnlyList<ItemDto>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken);
}