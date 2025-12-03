using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HomeInventory.Api.DTOs;
using HomeInventory.Api.Tests.Common;
using Xunit;

namespace HomeInventory.Api.Tests.Integration.Owners;

public class OwnersControllerNegativeTests : BaseIntegrationTest
{
    private const string BaseRoute = "/api/owners";

    public OwnersControllerNegativeTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_Creating_Owner_With_Empty_Name()
    {
        // Arrange
        var request = new CreateOwnerRequest
        {
            FullName = "", 
            Email = "valid@email.com"
        };

        // Act
        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_Return_NoContent_When_Deleting_NonExistent_Owner()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await Client.DeleteAsync($"{BaseRoute}/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_Return_NoContent_When_Updating_NonExistent_Owner()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        var updateRequest = new UpdateOwnerRequest
        {
            FullName = "New Name",
            Email = "new@email.com"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{nonExistentId}", updateRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}