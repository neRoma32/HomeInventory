namespace HomeInventory.Application.DTOs;

public record ItemDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Status { get; init; }
    public Guid RoomId { get; init; }
    public Guid CategoryId { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? RoomName { get; init; }
    public string? CategoryName { get; init; }
    public Guid OwnerId { get; init; }
    public string? OwnerName { get; init; }
    public bool HasWarranty { get; init; }
    public string? WarrantyProvider { get; init; }
    public DateTime? WarrantyExpiration { get; init; }
    public IReadOnlyList<string> Tags { get; init; } = Array.Empty<string>();
}