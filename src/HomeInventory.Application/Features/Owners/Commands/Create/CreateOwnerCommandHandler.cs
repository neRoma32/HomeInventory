using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using HomeInventory.Domain.Entities;
using MediatR;

namespace HomeInventory.Application.Features.Owners.Commands.Create;

public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, OwnerDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IOwnerQueries _ownerQueries;

    public CreateOwnerCommandHandler(IApplicationDbContext context, IOwnerQueries ownerQueries)
    {
        _context = context;
        _ownerQueries = ownerQueries;
    }

    public async Task<OwnerDto> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
    {
        var owner = Owner.New(Guid.NewGuid(), request.FullName, request.Email);

        await _context.Owners.AddAsync(owner, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return (await _ownerQueries.GetByIdAsync(owner.Id, cancellationToken))!;
    }
}