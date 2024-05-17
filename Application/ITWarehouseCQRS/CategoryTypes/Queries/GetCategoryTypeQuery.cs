using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CategoryTypes.Queries;
public class GetCategoryTypeQuery : IRequest<CategoryTypeVm>
{
    public GetCategoryTypeQuery(int i)
    {
        CategoryTypeId = i;
    }

    public int CategoryTypeId { get; set; }
}
