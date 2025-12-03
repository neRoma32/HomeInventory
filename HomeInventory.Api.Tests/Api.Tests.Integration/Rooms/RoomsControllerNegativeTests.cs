using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HomeInventory.Api.DTOs;
using HomeInventory.Api.Tests.Common;
using Xunit;

namespace HomeInventory.Api.Tests.Integration.Rooms;

public class RoomsControllerNegativeTests : BaseIntegrationTest
{
    private const string BaseRoute = "/api/rooms";

    public RoomsControllerNegativeTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_Creating_Room_With_Empty_Name()
    {
        // Arrange
        var request = new CreateRoomRequest { Name = "" };

        // Act
        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }


    [Fact]
    public async Task Should_Return_NoContent_When_Deleting_NonExistent_Room()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await Client.DeleteAsync($"{BaseRoute}/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_Return_NoContent_When_Updating_NonExistent_Room()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var updateRequest = new UpdateRoomRequest { Name = "New Name" };

        // Assert
        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{nonExistentId}", updateRequest);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}