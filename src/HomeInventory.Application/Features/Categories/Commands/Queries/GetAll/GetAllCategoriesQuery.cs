using HomeInventory.Application.DTOs;
using MediatR;

namespace HomeInventory.Application.Features.Categories.Queries.GetAll;

public record GetAllCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>;