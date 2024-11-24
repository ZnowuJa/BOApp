using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Categories.Queries;
public class GetAllCategoriesForSelectQuery : IRequest<IQueryable<CategoryVm>>
{
}
