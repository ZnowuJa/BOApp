using MediatR;
using Application.ViewModels;

namespace Application.ITWarehouseCQRS.CategoryTypes.Queries;
public class GetAllCategoryTypesQuery : IRequest<IQueryable<CategoryTypeVm>>
{
}
