using Application.ViewModels.General;
using MediatR;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetEmployeeByEnovaIdQuery : IRequest<EmployeeVm>
{
    public GetEmployeeByEnovaIdQuery(int i)
    {
        EmployeeId = i;
    }
    public int EmployeeId { get; set; }
}
