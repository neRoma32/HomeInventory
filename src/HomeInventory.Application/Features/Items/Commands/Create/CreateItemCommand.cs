using HomeInventory.Application.Common.Models;
using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Items.Commands.Create;

public record CreateItemCommand : IRequest<Result<Guid>>
{
    public required string Name { get; init; }
    public string Description { get; init; } = string.Empty; 
    public required Guid RoomId { get; init; }
    public required Guid CategoryId { get; init; }

    public required Guid OwnerId { get; init; }
}