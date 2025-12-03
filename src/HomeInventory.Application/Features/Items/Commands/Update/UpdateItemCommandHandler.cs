using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Application.Features.Items.Commands.Update;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public UpdateItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Items
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (item == null)
        {
            return Result<Guid>.Failure(Error.NotFound("Item", request.Id));
        }

        item.UpdateDetails(request.Name, request.Description);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(item.Id);
    }
}