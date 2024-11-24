using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Categories.Queries;
public class GetAllCategoriesQuery : IRequest<IQueryable<CategoryVm>>
{
}
