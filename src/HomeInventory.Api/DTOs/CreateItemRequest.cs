namespace HomeInventory.Api.DTOs;

public record CreateItemRequest
{
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
    public required Guid RoomId { get; init; }
    public required Guid CategoryId { get; init; }
    public required Guid OwnerId { get; init; }

}