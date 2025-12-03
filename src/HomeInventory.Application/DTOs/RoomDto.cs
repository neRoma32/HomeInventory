namespace HomeInventory.Application.DTOs;

public record RoomDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}