using HomeInventory.Domain.Entities;

namespace HomeInventory.Api.Tests.Data.Owners;

public static class OwnerData
{
    public static Owner Create()
    {
        var id = Guid.NewGuid();
        return Owner.New(
            id,
            $"Test Owner {id}",
            $"owner-{id}@example.com" 
        );
    }
}