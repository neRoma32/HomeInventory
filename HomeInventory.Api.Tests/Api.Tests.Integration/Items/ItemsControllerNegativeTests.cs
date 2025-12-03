using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HomeInventory.Api.DTOs;
using HomeInventory.Api.Tests.Common;
using Xunit;

namespace HomeInventory.Api.Tests.Integration.Items;

public class ItemsControllerNegativeTests : BaseIntegrationTest
{
    private const string BaseRoute = "/api/items";

    public ItemsControllerNegativeTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_Creating_With_Invalid_Data()
    {
        // Arrange 
        var request = new CreateItemRequest
        {
            Name = "",
            Description = "Invalid Item",
            RoomId = Guid.NewGuid(),    
            CategoryId = Guid.NewGuid(),
            OwnerId = Guid.NewGuid()
        };

        // Act
        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Return_NotFound_When_Getting_NonExistent_Item()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"{BaseRoute}/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_Return_NotFound_When_Deleting_NonExistent_Item()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await Client.DeleteAsync($"{BaseRoute}/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}