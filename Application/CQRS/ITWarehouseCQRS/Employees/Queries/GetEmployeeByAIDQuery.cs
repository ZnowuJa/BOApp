using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
public class GetEmployeeByAIDQuery : IRequest<EmployeeVm>
{
    public GetEmployeeByAIDQuery(Guid i)
    {
        EmployeeId = i;
    }
    public Guid EmployeeId { get; set; }
}
