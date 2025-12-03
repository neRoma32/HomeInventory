using HomeInventory.Domain.Entities;

namespace HomeInventory.Domain.Entities;

public class WarrantyInfo
{
    public Guid Id { get; private set; }
    public string Provider { get; private set; } 
    public string SupportContact { get; private set; }
    public DateTime ExpirationDate { get; private set; }

    public Guid ItemId { get; private set; }
    public Item Item { get; private set; }

    private WarrantyInfo(Guid id, string provider, string supportContact, DateTime expirationDate, Guid itemId)
    {
        Id = id;
        Provider = provider;
        SupportContact = supportContact;
        ExpirationDate = expirationDate;
        ItemId = itemId;
    }

    public static WarrantyInfo New(Guid id, string provider, string supportContact, DateTime expirationDate, Guid itemId)
    {
        return new WarrantyInfo(id, provider, supportContact, expirationDate, itemId);
    }

    public void UpdateDetails(string provider, string supportContact, DateTime expirationDate)
    {
        Provider = provider;
        SupportContact = supportContact;
        ExpirationDate = expirationDate;
    }
}