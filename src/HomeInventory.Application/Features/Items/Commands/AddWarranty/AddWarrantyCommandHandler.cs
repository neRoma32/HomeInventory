using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Application.Features.Items.Commands.AddWarranty;

public class AddWarrantyCommandHandler : IRequestHandler<AddWarrantyCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public AddWarrantyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(AddWarrantyCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Items
            .Include(i => i.Warranty)
            .FirstOrDefaultAsync(i => i.Id == request.ItemId, cancellationToken);

        if (item == null)
        {
            return Result<Guid>.Failure(Error.NotFound("Item", request.ItemId));
        }

        if (item.Warranty != null)
        {
            return Result<Guid>.Failure(new Error("Warranty.Exists", "Warranty already exists for this item."));
        }

        var warranty = WarrantyInfo.New(
            Guid.NewGuid(),
            request.Provider,
            request.SupportContact,
            request.ExpirationDate,
            request.ItemId
        );

        _context.WarrantyInfos.Add(warranty);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(warranty.Id);
    }
}