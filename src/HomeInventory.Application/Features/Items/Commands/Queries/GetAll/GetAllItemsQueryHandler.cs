using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Items.Queries.GetAll;

public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, IReadOnlyList<ItemDto>>
{
    private readonly IItemQueries _itemQueries;

    public GetAllItemsQueryHandler(IItemQueries itemQueries)
    {
        _itemQueries = itemQueries;
    }

    public async Task<IReadOnlyList<ItemDto>> Handle(
        GetAllItemsQuery request,
        CancellationToken cancellationToken)
    {
        return await _itemQueries.GetAllAsync(cancellationToken);
    }
}