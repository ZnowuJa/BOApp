using Application.ViewModels.General;
using MediatR;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetAllManagersAsEmployeesForSelectQuery : IRequest<IQueryable<EmployeeVm>>
{
}
