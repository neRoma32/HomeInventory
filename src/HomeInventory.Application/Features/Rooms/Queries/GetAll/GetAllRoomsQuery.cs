using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Rooms.Queries.GetAll;

public record GetAllRoomsQuery : IRequest<IReadOnlyList<RoomDto>>;