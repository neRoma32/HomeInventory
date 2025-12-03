using HomeInventory.Domain.Entities;

namespace HomeInventory.Api.Tests.Data.Rooms;

public static class RoomData
{
    public static Room Create()
    {
        return Room.New(Guid.NewGuid(), $"Test Room {Guid.NewGuid()}");
    }
}