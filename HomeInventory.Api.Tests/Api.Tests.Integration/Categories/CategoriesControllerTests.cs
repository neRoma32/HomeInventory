using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HomeInventory.Api.DTOs; 
using HomeInventory.Api.Tests.Common;
using HomeInventory.Api.Tests.Data.Categories;
using HomeInventory.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeInventory.Api.Tests.Integration.Categories;

public class CategoriesControllerTests : BaseIntegrationTest
{
    private const string BaseRoute = "/api/categories";

    public CategoriesControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_Create_Category()
    {
        // Arrange
        var request = new CreateCategoryRequest { Name = "Electronics" };

        // Act
        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var dto = await response.ToResponseModel<CategoryDto>();
        dto.Name.Should().Be(request.Name);

        // Перевірка в БД
        var dbEntity = await Context.Categories.FirstOrDefaultAsync(c => c.Id == dto.Id);
        dbEntity.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_Get_All_Categories()
    {
        // Arrange
        var category = CategoryData.Create();
        Context.Categories.Add(category);
        await SaveChangesAsync();

        // Act
        var response = await Client.GetAsync(BaseRoute);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var list = await response.ToResponseModel<List<CategoryDto>>();
        list.Should().Contain(c => c.Id == category.Id);
    }

    [Fact]
    public async Task Should_Update_Category()
    {
        // Arrange
        var category = CategoryData.Create();
        Context.Categories.Add(category);
        await SaveChangesAsync();

        var updateRequest = new UpdateCategoryRequest { Name = "Updated Category Name" };

        // Act
        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{category.Id}", updateRequest);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var dbEntity = await Context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == category.Id);
        dbEntity!.Name.Should().Be("Updated Category Name");
    }

    [Fact]
    public async Task Should_Delete_Category()
    {
        // Arrange
        var category = CategoryData.Create();
        Context.Categories.Add(category);
        await SaveChangesAsync();

        // Act
        var response = await Client.DeleteAsync($"{BaseRoute}/{category.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var exists = await Context.Categories.AnyAsync(c => c.Id == category.Id);
        exists.Should().BeFalse();
    }
}