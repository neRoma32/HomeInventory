using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Tags.Queries.GetAll;

public record GetAllTagsQuery : IRequest<IReadOnlyList<TagDto>>;