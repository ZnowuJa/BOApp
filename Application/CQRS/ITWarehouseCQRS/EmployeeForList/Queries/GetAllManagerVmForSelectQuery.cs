using Application.ViewModels;
using Application.ViewModels.Employees;
using MediatR;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetAllManagerVmForSelectQuery : IRequest<IQueryable<ManagerVm>>
{
}
