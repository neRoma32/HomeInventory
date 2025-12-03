using HomeInventory.Application.Common.Models;
using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Items.Queries.GetById;

public record GetItemByIdQuery(Guid Id) : IRequest<Optional<ItemDto>>;