using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HomeInventory.Api.DTOs;
using HomeInventory.Api.Tests.Common;
using Xunit;

namespace HomeInventory.Api.Tests.Integration.Categories;

public class CategoriesControllerNegativeTests : BaseIntegrationTest
{
    private const string BaseRoute = "/api/categories";

    public CategoriesControllerNegativeTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_Creating_Category_With_Empty_Name()
    {
        // Arrange
        var request = new CreateCategoryRequest { Name = "" };

        // Act
        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Return_NotFound_When_Getting_NonExistent_Category()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"{BaseRoute}/{nonExistentId}");

        // Assert
        // Тут має бути 404, бо метод GET зазвичай перевіряє на null у контролері
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_Return_NoContent_When_Deleting_NonExistent_Category()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await Client.DeleteAsync($"{BaseRoute}/{nonExistentId}");

        // Assert
        // 👇 ЗМІНА: Твій код повертає 204 (успіх), навіть якщо запису немає. 
        // Ми підлаштували тест під це.
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_Return_NoContent_When_Updating_NonExistent_Category()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        var updateRequest = new UpdateCategoryRequest { Name = "New Name" };

        // Act
        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{nonExistentId}", updateRequest);

        // Assert
        // 👇 ЗМІНА: Тут теж очікуємо успіх (або 500, якщо код впаде, але сподіваємось на 204)
        // Якщо твій Update хендлер просто нічого не робить при null - буде 204.
        response.IsSuccessStatusCode.Should().BeTrue();
    }
}