using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Owners.Commands.Create;

public record CreateOwnerCommand : IRequest<OwnerDto>
{
    public required string FullName { get; init; }
    public required string Email { get; init; }
}