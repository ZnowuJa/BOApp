using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
public class GetManagerQuery : IRequest<ManagerVm>
{
    public GetManagerQuery(int i)
    {
        EmployeeId = i;
    }
    public int EmployeeId { get; set; }
}
