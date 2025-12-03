using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Tags.Queries.GetById;
public record GetTagByIdQuery : IRequest<TagDto?>
{
    public Guid Id { get; init; }
}