using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Categories.Queries.GetById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
{
    private readonly ICategoryQueries _categoryQueries;

    public GetCategoryByIdQueryHandler(ICategoryQueries categoryQueries)
    {
        _categoryQueries = categoryQueries;
    }

    public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _categoryQueries.GetByIdAsync(request.Id, cancellationToken);
    }
}