namespace HomeInventory.Api.DTOs;

public record UpdateCategoryRequest
{
    public required string Name { get; init; }
}