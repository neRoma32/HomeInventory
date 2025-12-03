using HomeInventory.Application.Common.Interfaces;
using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Tags.Queries.GetAll;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, IReadOnlyList<TagDto>>
{
    private readonly ITagQueries _tagQueries;

    public GetAllTagsQueryHandler(ITagQueries tagQueries)
    {
        _tagQueries = tagQueries;
    }

    public async Task<IReadOnlyList<TagDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        return await _tagQueries.GetAllAsync(cancellationToken);
    }
}