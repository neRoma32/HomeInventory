using HomeInventory.Application.Common.Models;
using MediatR;

namespace HomeInventory.Application.Features.Items.Commands.AddWarranty;

public record AddWarrantyCommand : IRequest<Result<Guid>>
{
    public Guid ItemId { get; init; }
    public required string Provider { get; init; }
    public required string SupportContact { get; init; }
    public DateTime ExpirationDate { get; init; }
}