namespace HomeInventory.Api.DTOs;

public record UpdateItemRequest
{
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
}