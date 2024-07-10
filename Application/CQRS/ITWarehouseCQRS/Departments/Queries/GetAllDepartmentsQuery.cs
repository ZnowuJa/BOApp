using Application.ViewModels.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Departments.Queries;
public class GetAllDepartmentsQuery : IRequest<IQueryable<DepartmentVm>>
{
    public GetAllDepartmentsQuery()
    {
        
    }    

}
