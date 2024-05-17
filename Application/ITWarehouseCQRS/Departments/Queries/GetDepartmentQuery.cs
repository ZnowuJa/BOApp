using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Departments.Queries;
public class GetDepartmentQuery : IRequest<DepartmentVm>
{
    public GetDepartmentQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
    

}
