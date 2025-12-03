using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Application.Features.Items.Commands.Delete;

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public DeleteItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Items
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (item == null)
        {
            return Result<Guid>.Failure(Error.NotFound("Item", request.Id));
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(item.Id);
    }
}