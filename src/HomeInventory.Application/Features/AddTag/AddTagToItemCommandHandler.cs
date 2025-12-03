using HomeInventory.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Application.Features.Items.Commands.AddTag;

public class AddTagToItemCommandHandler : IRequestHandler<AddTagToItemCommand>
{
    private readonly IApplicationDbContext _context;

    public AddTagToItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddTagToItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Items
            .Include(i => i.Tags)
            .FirstOrDefaultAsync(i => i.Id == request.ItemId, cancellationToken);

        if (item == null)
        {
            throw new Exception($"Item with ID {request.ItemId} not found.");
        }

        var tag = await _context.Tags
            .FirstOrDefaultAsync(t => t.Id == request.TagId, cancellationToken);

        if (tag == null)
        {
            throw new Exception($"Tag with ID {request.TagId} not found.");
        }

        item.AddTag(tag);

        await _context.SaveChangesAsync(cancellationToken);
    }
}