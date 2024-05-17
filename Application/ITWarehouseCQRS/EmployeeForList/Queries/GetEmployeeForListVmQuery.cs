using Application.ViewModels;
using Application.ViewModels.Employees;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetEmployeeForListVmQuery : IRequest<EmployeeForListVm>
{
    public GetEmployeeForListVmQuery(int i)
    {
        EmployeeId = i;
    }
    public int EmployeeId { get; set; }
}
