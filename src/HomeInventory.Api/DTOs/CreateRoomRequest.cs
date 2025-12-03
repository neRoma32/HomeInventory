namespace HomeInventory.Api.DTOs;
public record CreateRoomRequest
{
    public required string Name { get; init; }
}