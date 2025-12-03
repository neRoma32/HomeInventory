using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.Common.Models; 
using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Items.Queries.GetById;

public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, Optional<ItemDto>>
{
    private readonly IItemQueries _itemQueries;

    public GetItemByIdQueryHandler(IItemQueries itemQueries)
    {
        _itemQueries = itemQueries;
    }

    public async Task<Optional<ItemDto>> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        return await _itemQueries.GetByIdAsync(request.Id, cancellationToken);
    }
}