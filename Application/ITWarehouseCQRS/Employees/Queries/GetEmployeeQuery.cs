using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetEmployeeQuery : IRequest<EmployeeVm>
{
    public GetEmployeeQuery(int i)
    {
        EmployeeId = i;
    }
    public int EmployeeId { get; set; }
}
