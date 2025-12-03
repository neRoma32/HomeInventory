using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Domain.Entities;
using MediatR;

namespace HomeInventory.Application.Features.Tags.Commands.Create;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = Tag.New(Guid.NewGuid(), request.Name);

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return tag.Id;
    }
}