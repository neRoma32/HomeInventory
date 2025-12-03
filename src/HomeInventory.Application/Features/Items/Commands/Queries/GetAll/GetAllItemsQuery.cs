using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Items.Queries.GetAll;

public record GetAllItemsQuery : IRequest<IReadOnlyList<ItemDto>>;