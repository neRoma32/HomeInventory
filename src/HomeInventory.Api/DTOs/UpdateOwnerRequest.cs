namespace HomeInventory.Api.DTOs
{
    public record UpdateOwnerRequest
    {
        public required string FullName { get; init; }
        public required string Email { get; init; }
    }
}
