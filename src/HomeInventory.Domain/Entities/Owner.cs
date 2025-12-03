namespace HomeInventory.Domain.Entities;

public class Owner
{
    public Guid Id { get; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; private set; }

    private Owner(Guid id, string fullName, string email)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }

    public static Owner New(Guid id, string fullName, string email)
    {
        return new Owner(id, fullName, email);
    }

    public void Update(string fullName, string email)
    {
        FullName = fullName;
        Email = email;
        UpdatedAt = DateTime.UtcNow;
    }
}