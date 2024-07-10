using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.ITWarehouseCQRS.CategoryTypes.Queries;
public class GetAllCategoryTypesQuery : IRequest<IQueryable<CategoryTypeVm>>
{
}
