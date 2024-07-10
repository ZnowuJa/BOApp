using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.General.Employees2Select.Queries;
public class GetAllEmployees2SelectQuery : IRequest<IQueryable<Employee2Select>>
{
}
