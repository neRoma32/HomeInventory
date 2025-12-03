using HomeInventory.Application.DTOs;

namespace HomeInventory.Application.Common.Interfaces;

public interface IRoomQueries
{
    Task<RoomDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyList<RoomDto>> GetAllAsync(CancellationToken cancellationToken);
}