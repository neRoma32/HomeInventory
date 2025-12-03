using HomeInventory.Domain.Enums;

namespace HomeInventory.Domain.Entities;

public class Item
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ItemStatus Status { get; private set; }

    public Guid RoomId { get; private set; }
    public Guid CategoryId { get; private set; }

    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }
    public Guid OwnerId { get; private set; }
    public Owner Owner { get; private set; }

    public Room Room { get; private set; }
    public Category Category { get; private set; }

    public WarrantyInfo? Warranty { get; private set; }

    private readonly List<Tag> _tags = new();
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
    private Item(
        Guid id,
        string name,
        string description,
        Guid roomId,
        Guid categoryId,
        Guid ownerId)
    {
        Id = id;
        Name = name;
        Description = description;
        RoomId = roomId;
        OwnerId = ownerId;
        CategoryId = categoryId;
        Status = ItemStatus.InPlace; 
        CreatedAt = DateTime.UtcNow;
    }

    public static Item New(
        Guid id,
        string name,
        string description,
        Guid roomId,
        Guid categoryId,
        Guid ownerId)
    {
        return new Item(id, name, description, roomId, categoryId, ownerId);
    }


    public void UpdateDetails(string name, string description)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeStatus(ItemStatus newStatus)
    {
        if (Status == ItemStatus.Discarded)
        {
            return;
        }

        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MoveToRoom(Guid newRoomId)
    {
        if (newRoomId == Guid.Empty || newRoomId == RoomId)
        {
            return;
        }

        RoomId = newRoomId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeCategory(Guid newCategoryId)
    {
        if (newCategoryId == Guid.Empty || newCategoryId == CategoryId)
        {
            return;
        }

        CategoryId = newCategoryId;
        UpdatedAt = DateTime.UtcNow;
    }
    public void AddTag(Tag tag)
    {
        if (!_tags.Any(t => t.Id == tag.Id))
        {
            _tags.Add(tag);
        }
    }
}