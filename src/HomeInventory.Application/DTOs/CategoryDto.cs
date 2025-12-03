namespace HomeInventory.Application.DTOs;

public record CategoryDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
}