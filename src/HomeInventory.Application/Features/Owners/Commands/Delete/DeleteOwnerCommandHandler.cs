using HomeInventory.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Application.Features.Owners.Commands.Delete;

public class DeleteOwnerCommandHandler : IRequestHandler<DeleteOwnerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteOwnerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
    {
        var hasItems = await _context.Items
            .AnyAsync(i => i.OwnerId == request.Id, cancellationToken);

        if (hasItems)
        {
            throw new InvalidOperationException("Cannot delete owner with assigned items.");
        }

        var owner = await _context.Owners
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        if (owner is not null)
        {
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}