using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
public class GetAllEmployeesForSelectQuery : IRequest<IQueryable<EmployeeVm>>
{
}

