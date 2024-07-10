using Application.ViewModels;
using Application.ViewModels.Employees;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetManagerVmQuery : IRequest<ManagerVm>
{
    public GetManagerVmQuery(int i)
    {
        ManagerId = i;
    }
    public int ManagerId { get; set; }
}
