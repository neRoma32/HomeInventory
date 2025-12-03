using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Owners.Queries.GetAll;

public class GetAllOwnersQueryHandler : IRequestHandler<GetAllOwnersQuery, IReadOnlyList<OwnerDto>>
{
    private readonly IOwnerQueries _ownerQueries;

    public GetAllOwnersQueryHandler(IOwnerQueries ownerQueries)
    {
        _ownerQueries = ownerQueries;
    }

    public async Task<IReadOnlyList<OwnerDto>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
    {
        return await _ownerQueries.GetAllAsync(cancellationToken);
    }
}