using HomeInventory.Domain.Entities;

namespace HomeInventory.Domain.Entities;

public class Tag
{
    public Guid Id { get; }
    public string Name { get; private set; }

    private readonly List<Item> _items = new();
    public IReadOnlyCollection<Item> Items => _items.AsReadOnly();

    public DateTime CreatedAt { get; }

    private Tag(Guid id, string name)
    {
        Id = id;
        Name = name;
        CreatedAt = DateTime.UtcNow;
    }

    public static Tag New(Guid id, string name)
    {
        return new Tag(id, name);
    }

    public void Update(string name)
    {
        Name = name;
    }

}