using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Tags.Queries.GetById;

public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, TagDto?>
{
    private readonly ITagQueries _tagQueries;

    public GetTagByIdQueryHandler(ITagQueries tagQueries)
    {
        _tagQueries = tagQueries;
    }

    public async Task<TagDto?> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        return await _tagQueries.GetByIdAsync(request.Id, cancellationToken);
    }
}