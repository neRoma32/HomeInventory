using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Owners.Queries.GetAll;

public record GetAllOwnersQuery : IRequest<IReadOnlyList<OwnerDto>>;