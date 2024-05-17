using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Queries;
public class GetEmployeeTypeQuery : IRequest<EmployeeTypeVm>
{
    public GetEmployeeTypeQuery(int i)
    {
        EmployeeTypeId = i;
    }

    public int EmployeeTypeId { get; set; }
}
