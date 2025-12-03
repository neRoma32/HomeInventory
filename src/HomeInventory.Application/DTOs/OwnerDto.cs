namespace HomeInventory.Application.DTOs;

public record OwnerDto
{
    public Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
}