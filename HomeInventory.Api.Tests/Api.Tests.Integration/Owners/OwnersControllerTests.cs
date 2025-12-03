using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HomeInventory.Api.DTOs;
using HomeInventory.Api.Tests.Common;
using HomeInventory.Api.Tests.Data.Owners;
using HomeInventory.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeInventory.Api.Tests.Integration.Owners;

public class OwnersControllerTests : BaseIntegrationTest
{
    private const string BaseRoute = "/api/owners";

    public OwnersControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_Create_Owner()
    {
        // Arrange
        var request = new CreateOwnerRequest
        {
            FullName = "John Tester",
            Email = "john.tester@example.com"
        };

        // Act
        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var dto = await response.ToResponseModel<OwnerDto>();
        dto.Email.Should().Be(request.Email);

        var dbEntity = await Context.Owners.FirstOrDefaultAsync(o => o.Id == dto.Id);
        dbEntity.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_Get_All_Owners()
    {
        // Arrange
        var owner = OwnerData.Create();
        Context.Owners.Add(owner);
        await SaveChangesAsync();

        // Act
        var response = await Client.GetAsync(BaseRoute);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var list = await response.ToResponseModel<List<OwnerDto>>();
        list.Should().Contain(o => o.Id == owner.Id);
    }

    [Fact]
    public async Task Should_Update_Owner()
    {
        // Arrange
        var owner = OwnerData.Create();
        Context.Owners.Add(owner);
        await SaveChangesAsync();

        var updateRequest = new UpdateOwnerRequest
        {
            FullName = "Updated Name",
            Email = "updated@email.com"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{owner.Id}", updateRequest);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var dbEntity = await Context.Owners.AsNoTracking().FirstOrDefaultAsync(o => o.Id == owner.Id);
        dbEntity!.FullName.Should().Be("Updated Name");
        dbEntity.Email.Should().Be("updated@email.com");
    }

    [Fact]
    public async Task Should_Delete_Owner()
    {
        // Arrange
        var owner = OwnerData.Create();
        Context.Owners.Add(owner);
        await SaveChangesAsync();

        // Act
        var response = await Client.DeleteAsync($"{BaseRoute}/{owner.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var exists = await Context.Owners.AnyAsync(o => o.Id == owner.Id);
        exists.Should().BeFalse();
    }
}