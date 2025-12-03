using HomeInventory.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace HomeInventory.Application.Features.Categories.Commands.Delete;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var hasItems = await _context.Items
            .AnyAsync(i => i.CategoryId == request.Id, cancellationToken);

        if (hasItems)
        {
            throw new InvalidOperationException("Cannot delete category with items.");
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (category is not null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}