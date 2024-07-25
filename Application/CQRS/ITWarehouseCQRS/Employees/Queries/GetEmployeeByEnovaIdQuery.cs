using Application.ViewModels.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetEmployeeByEnovaIdQuery : IRequest<EmployeeVm>
{
    public GetEmployeeByEnovaIdQuery(int i)
    {
        EmployeeId = i;
    }
    public int EmployeeId { get; set; }
}
