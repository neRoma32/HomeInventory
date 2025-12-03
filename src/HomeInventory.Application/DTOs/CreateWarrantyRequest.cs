namespace HomeInventory.Api.DTOs;

public record CreateWarrantyRequest(
    string Provider,
    string SupportContact,
    DateTime ExpirationDate
);