using HomeInventory.Application.Common.Models;
using MediatR;

namespace HomeInventory.Application.Features.Items.Commands.Delete;

public record DeleteItemCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; init; }
}