using Application.ViewModels.General;
using MediatR;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetEmployeeQuery : IRequest<EmployeeVm>
{
    public GetEmployeeQuery(int i)
    {
        EmployeeId = i;
    }
    public int EmployeeId { get; set; }
}
