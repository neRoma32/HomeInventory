namespace HomeInventory.Api.DTOs;

public record UpdateRoomRequest
{
    public required string Name { get; init; }
}