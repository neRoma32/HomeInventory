using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.Common.Models;
using HomeInventory.Domain.Entities;
using MediatR;

namespace HomeInventory.Application.Features.Items.Commands.Create;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;

    public CreateItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Guid>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var item = Item.New(
            id: Guid.NewGuid(),
            name: request.Name,
            description: request.Description,
            roomId: request.RoomId,
            categoryId: request.CategoryId,
            ownerId: request.OwnerId
        );

        await _context.Items.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(item.Id);
    }
}