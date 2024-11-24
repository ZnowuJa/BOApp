using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.CategoryTypes.Queries;
public class GetCategoryTypeQuery : IRequest<CategoryTypeVm>
{
    public GetCategoryTypeQuery(int i)
    {
        CategoryTypeId = i;
    }

    public int CategoryTypeId { get; set; }
}
