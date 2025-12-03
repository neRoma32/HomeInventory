using HomeInventory.Domain.Entities;

namespace HomeInventory.Api.Tests.Data.Categories;

public static class CategoryData
{
    public static Category Create()
    {
        return Category.New(Guid.NewGuid(), $"Test Category {Guid.NewGuid()}");
    }
}