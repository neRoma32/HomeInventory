using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using HomeInventory.Api.DTOs;
using HomeInventory.Api.Tests.Common;
using HomeInventory.Api.Tests.Data.Categories;
using HomeInventory.Api.Tests.Data.Items;
using HomeInventory.Api.Tests.Data.Owners;
using HomeInventory.Api.Tests.Data.Rooms;
using HomeInventory.Application.DTOs;
using HomeInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeInventory.Api.Tests.Api.Tests.Integration.Items;

public class ItemsControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private const string BaseRoute = "/api/items";

    private Guid _roomId;
    private Guid _categoryId;
    private Guid _ownerId;

    public ItemsControllerTests(IntegrationTestWebFactory factory) : base(factory)
    {
    }

    public async Task InitializeAsync()
    {
        var room = RoomData.Create();
        var category = CategoryData.Create();
        var owner = OwnerData.Create();

        Context.Rooms.Add(room);
        Context.Categories.Add(category);
        Context.Owners.Add(owner);

        await SaveChangesAsync();

        _roomId = room.Id;
        _categoryId = category.Id;
        _ownerId = owner.Id;
    }

    public Task DisposeAsync() => Task.CompletedTask;


    [Fact]
    public async Task Should_Create_Item()
    {
        // Arrange
        var request = new CreateItemRequest
        {
            Name = "Integration Test Laptop",
            Description = "Powerful device",
            RoomId = _roomId,
            CategoryId = _categoryId,
            OwnerId = _ownerId
        };

        // Act
        var response = await Client.PostAsJsonAsync(BaseRoute, request);

        // Assert (Відповідь)
        response.IsSuccessStatusCode.Should().BeTrue();

        var createResponse = await response.ToResponseModel<CreateResponse>();
        createResponse.Id.Should().NotBeEmpty();

        var dbItem = await Context.Items.FirstOrDefaultAsync(x => x.Id == createResponse.Id);
        dbItem.Should().NotBeNull();
        dbItem!.Name.Should().Be(request.Name);
        dbItem.Description.Should().Be(request.Description);
    }


    [Fact]
    public async Task Should_Get_All_Items()
    {
        // Arrange
        var item = ItemData.Create(_roomId, _categoryId, _ownerId);
        Context.Items.Add(item);
        await SaveChangesAsync();

        // Act
        var response = await Client.GetAsync(BaseRoute);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var items = await response.ToResponseModel<List<ItemDto>>();

        items.Should().Contain(x => x.Id == item.Id);
    }

    [Fact]
    public async Task Should_Get_Item_By_Id()
    {
        // Arrange
        var item = ItemData.Create(_roomId, _categoryId, _ownerId);
        Context.Items.Add(item);
        await SaveChangesAsync();

        // Act
        var response = await Client.GetAsync($"{BaseRoute}/{item.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var itemDto = await response.ToResponseModel<ItemDto>();
        itemDto.Id.Should().Be(item.Id);
    }


    [Fact]
    public async Task Should_Update_Item()
    {
        // Arrange
        var item = ItemData.Create(_roomId, _categoryId, _ownerId);
        Context.Items.Add(item);
        await SaveChangesAsync();

        var updateRequest = new UpdateItemRequest
        {
            Name = "Updated Name",
            Description = "Updated Description"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"{BaseRoute}/{item.Id}", updateRequest);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue(); 

        var dbItem = await Context.Items.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
        dbItem!.Name.Should().Be("Updated Name");
        dbItem.Description.Should().Be("Updated Description");
    }


    [Fact]
    public async Task Should_Delete_Item()
    {
        // Arrange
        var item = ItemData.Create(_roomId, _categoryId, _ownerId);
        Context.Items.Add(item);
        await SaveChangesAsync();

        // Act
        var response = await Client.DeleteAsync($"{BaseRoute}/{item.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var exists = await Context.Items.AnyAsync(x => x.Id == item.Id);
        exists.Should().BeFalse();
    }


    [Fact]
    public async Task Should_Add_Warranty_To_Item()
    {
        // Arrange
        var item = ItemData.Create(_roomId, _categoryId, _ownerId);
        Context.Items.Add(item);
        await SaveChangesAsync();

        var warrantyRequest = new CreateWarrantyRequest(
            Provider: "Rozetka",
            SupportContact: "+380000000000",
            ExpirationDate: DateTime.UtcNow.AddYears(1)
        );

        // Act
        var response = await Client.PostAsJsonAsync($"{BaseRoute}/{item.Id}/warranty", warrantyRequest);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var warranty = await Context.WarrantyInfos.FirstOrDefaultAsync(w => w.ItemId == item.Id);
        warranty.Should().NotBeNull();
        warranty!.Provider.Should().Be("Rozetka");
    }

    private record CreateResponse(Guid Id);
}