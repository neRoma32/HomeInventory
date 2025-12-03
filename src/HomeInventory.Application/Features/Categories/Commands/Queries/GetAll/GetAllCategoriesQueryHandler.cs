using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Categories.Queries.GetAll;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IReadOnlyList<CategoryDto>>
{
    private readonly ICategoryQueries _categoryQueries;

    public GetAllCategoriesQueryHandler(ICategoryQueries categoryQueries)
    {
        _categoryQueries = categoryQueries;
    }

    public async Task<IReadOnlyList<CategoryDto>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        return await _categoryQueries.GetAllAsync(cancellationToken);
    }
}