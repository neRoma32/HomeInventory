namespace HomeInventory.Api.DTOs;

public record CreateCategoryRequest
{
    public required string Name { get; init; }
}