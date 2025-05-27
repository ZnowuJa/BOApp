using Application.ViewModels.General;
using MediatR;

namespace Application.ITWarehouseCQRS.Departments.Queries;
public class GetDepartmentQuery : IRequest<DepartmentVm>
{
    public GetDepartmentQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
    

}
