using HomeInventory.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeInventory.Application.Features.Owners.Commands.Update;

public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateOwnerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
    {
        var owner = await _context.Owners
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        if (owner is not null)
        {
            owner.Update(request.FullName, request.Email);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}