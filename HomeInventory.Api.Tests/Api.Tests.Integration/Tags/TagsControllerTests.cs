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
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace HomeInventory.Api.Tests.Integration.Tags;

public class TagsControllerTests : BaseIntegrationTest, IAsyncLifetime
{
    private Guid _itemId;

    public TagsControllerTests(IntegrationTestWebFactory factory) : base(factory)
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

        var item = ItemData.Create(room.Id, category.Id, owner.Id);
        Context.Items.Add(item);
        await SaveChangesAsync();

        _itemId = item.Id;
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task Should_Create_Tag()
    {
        // Arrange
        var request = new CreateTagRequest("Fragile");

        // Act
        var response = await Client.PostAsJsonAsync("/api/tags", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var tag = await Context.Tags.FirstOrDefaultAsync(t => t.Name == "Fragile");
        tag.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_Add_Tag_To_Item()
    {
        // Arrange
        var tag = Tag.New(Guid.NewGuid(), "Urgent");
        Context.Tags.Add(tag);
        await SaveChangesAsync();
        var request = new AddTagToItemRequest(tag.Id);

        // Act
        var response = await Client.PostAsJsonAsync($"/api/items/{_itemId}/tags", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var dbItem = await Context.Items
            .Include(i => i.Tags)
            .FirstOrDefaultAsync(i => i.Id == _itemId);

        dbItem!.Tags.Should().Contain(t => t.Id == tag.Id);
    }


    [Fact]
    public async Task Should_Get_All_Tags()
    {
        // Arrange
        var tag = Tag.New(Guid.NewGuid(), "Cool Tag");
        Context.Tags.Add(tag);
        await SaveChangesAsync();

        // Act
        var response = await Client.GetAsync("/api/tags");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var tags = await response.ToResponseModel<List<TagDto>>();

        tags.Should().Contain(t => t.Id == tag.Id);
    }

    [Fact]
    public async Task Should_Get_Tag_By_Id()
    {
        // Arrange
        var tag = Tag.New(Guid.NewGuid(), "Unique Tag");
        Context.Tags.Add(tag);
        await SaveChangesAsync();

        // Act
        var response = await Client.GetAsync($"/api/tags/{tag.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var tagDto = await response.ToResponseModel<TagDto>();
        tagDto.Name.Should().Be("Unique Tag");
    }

    [Fact]
    public async Task Should_Update_Tag()
    {
        // Arrange
        var tag = Tag.New(Guid.NewGuid(), "Old Name");
        Context.Tags.Add(tag);
        await SaveChangesAsync();

        var updateRequest = new UpdateTagRequest("New Name"); 

        // Act
        var response = await Client.PutAsJsonAsync($"/api/tags/{tag.Id}", updateRequest);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var dbTag = await Context.Tags.AsNoTracking().FirstOrDefaultAsync(t => t.Id == tag.Id);
        dbTag!.Name.Should().Be("New Name");
    }


    [Fact]
    public async Task Should_Delete_Tag()
    {
        // Arrange
        var tag = Tag.New(Guid.NewGuid(), "To Delete");
        Context.Tags.Add(tag);
        await SaveChangesAsync();

        // Act
        var response = await Client.DeleteAsync($"/api/tags/{tag.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var exists = await Context.Tags.AnyAsync(t => t.Id == tag.Id);
        exists.Should().BeFalse();
    }
}