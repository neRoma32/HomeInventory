using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using HomeInventory.Domain.Entities;
using MediatR;

namespace HomeInventory.Application.Features.Categories.Commands.Create;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICategoryQueries _categoryQueries;

    public CreateCategoryCommandHandler(IApplicationDbContext context, ICategoryQueries categoryQueries)
    {
        _context = context;
        _categoryQueries = categoryQueries;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.New(
            id: Guid.NewGuid(),
            name: request.Name);

        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var categoryDto = await _categoryQueries.GetByIdAsync(category.Id, cancellationToken);

        return categoryDto!;
    }
}