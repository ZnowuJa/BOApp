using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetAllEmployeesForSelectQuery : IRequest<IQueryable<EmployeeVm>>
{
}

