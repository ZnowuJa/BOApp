using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Categories.Queries;
public class GetAllCategoriesQuery : IRequest<IQueryable<CategoryVm>>
{
}
