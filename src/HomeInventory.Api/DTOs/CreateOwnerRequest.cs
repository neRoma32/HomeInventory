namespace HomeInventory.Api.DTOs;

public record CreateOwnerRequest
{
    public required string FullName { get; init; }
    public required string Email { get; init; }
}