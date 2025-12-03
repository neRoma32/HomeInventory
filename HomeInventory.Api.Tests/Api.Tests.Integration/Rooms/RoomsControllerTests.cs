using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HomeInventory.Api.DTOs; 
using HomeInventory.Api.Tests.Common;
using HomeInventory.Api.Tests.Data.Rooms;
using HomeInventory.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeInventory.Api.Tests.Integration.Rooms;

public class RoomsControllerTests : BaseIntegrationTest
{
    private const string BaseRoute = "/api/rooms";

    public RoomsControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_Create_Room()
    {
        // Arrange
        var request = new CreateRoomRequest
        {
            Name = "Kitchen"
        };

        // Act
        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var roomDto = await response.ToResponseModel<RoomDto>();
        roomDto.Name.Should().Be(request.Name);

        var dbRoom = await Context.Rooms.FirstOrDefaultAsync(r => r.Id == roomDto.Id);
        dbRoom.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_Get_All_Rooms()
    {
        // Arrange
        var room = RoomData.Create();
        Context.Rooms.Add(room);
        await SaveChangesAsync();

        // Act
        var response = await Client.GetAsync(BaseRoute);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var rooms = await response.ToResponseModel<List<RoomDto>>();
        rooms.Should().Contain(r => r.Id == room.Id);
    }

    [Fact]
    public async Task Should_Update_Room()
    {
        // Arrange
        var room = RoomData.Create();
        Context.Rooms.Add(room);
        await SaveChangesAsync();

        var updateRequest = new UpdateRoomRequest { Name = "Updated Kitchen" };

        // Act
        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{room.Id}", updateRequest);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var dbRoom = await Context.Rooms.AsNoTracking().FirstOrDefaultAsync(r => r.Id == room.Id);
        dbRoom!.Name.Should().Be("Updated Kitchen");
    }

    [Fact]
    public async Task Should_Delete_Room()
    {
        // Arrange
        var room = RoomData.Create();
        Context.Rooms.Add(room);
        await SaveChangesAsync();

        // Act
        var response = await Client.DeleteAsync($"{BaseRoute}/{room.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var exists = await Context.Rooms.AnyAsync(r => r.Id == room.Id);
        exists.Should().BeFalse();
    }
}