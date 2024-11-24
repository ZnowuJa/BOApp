using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.General.Employees2Select.Queries;
public class GetAllEmployees2SelectQuery : IRequest<IQueryable<Employee2Select>>
{
}
