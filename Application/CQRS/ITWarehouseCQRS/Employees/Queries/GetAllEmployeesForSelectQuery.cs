using Application.ViewModels.General;
using MediatR;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetAllEmployeesForSelectQuery : IRequest<IQueryable<EmployeeVm>>
{
}

