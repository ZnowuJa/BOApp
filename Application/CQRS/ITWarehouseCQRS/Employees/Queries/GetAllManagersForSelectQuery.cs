using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Queries;
public class GetAllManagersForSelectQuery : IRequest<IQueryable<ManagerVm>>
{
}
