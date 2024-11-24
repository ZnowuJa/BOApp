using Application.ViewModels.General;
using MediatR;

namespace Application.ITWarehouseCQRS.Departments.Queries;
public class GetAllDepartmentsQuery : IRequest<IQueryable<DepartmentVm>>
{
    public GetAllDepartmentsQuery()
    {
        
    }    

}
