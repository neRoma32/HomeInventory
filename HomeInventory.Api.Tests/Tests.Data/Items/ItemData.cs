using HomeInventory.Domain.Entities;
using HomeInventory.Domain.Enums;

namespace HomeInventory.Api.Tests.Data.Items;

public static class ItemData
{
    public static Item Create(Guid roomId, Guid categoryId, Guid ownerId)
    {
        return Item.New(
            Guid.NewGuid(),
            "Test Item",
            "Test Description",
            roomId,
            categoryId,
            ownerId
        );
    }
}