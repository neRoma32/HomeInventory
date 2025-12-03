using HomeInventory.Application.Common.Models;
using MediatR;

namespace HomeInventory.Application.Features.Items.Commands.Update;

public record UpdateItemCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty;
}