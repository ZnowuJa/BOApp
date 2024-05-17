using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Categories.Queries;
public class GetCategoryQuery : IRequest<CategoryVm>
{
    public GetCategoryQuery(int i)
    {
        CategoryId = i;
    }

    public int CategoryId { get; set; }
}