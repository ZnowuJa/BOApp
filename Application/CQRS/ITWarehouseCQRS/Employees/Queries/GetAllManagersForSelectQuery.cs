using Application.ViewModels.General;
using MediatR;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetAllManagersForSelectQuery : IRequest<IQueryable<ManagerVm>>
{
}
