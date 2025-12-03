using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Categories.Queries.GetById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;