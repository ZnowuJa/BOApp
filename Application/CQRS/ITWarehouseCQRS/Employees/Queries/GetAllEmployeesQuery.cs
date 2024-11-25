using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
public class GetAllEmployeesQuery : IRequest<IQueryable<EmployeeVm>>
{
}
