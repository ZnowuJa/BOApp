using MediatR;
using Application.ViewModels;

namespace Application.ITWarehouseCQRS.CategoryTypes.Queries;
public class GetAllCategoryTypesForSelectQuery : IRequest<IQueryable<CategoryTypeVm>>
{
}
