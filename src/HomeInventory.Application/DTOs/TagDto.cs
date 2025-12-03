namespace HomeInventory.Application.DTOs;

public record TagDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public DateTime CreatedAt { get; init; }
}