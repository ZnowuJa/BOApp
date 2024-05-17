using Application.ViewModelsForForms;
using MediatR;

namespace Application.ITWarehouseCQRS.Employees.Queries;
public class GetEmployeeGridModelListQuery : IRequest<IQueryable<EmployeeGridModel>>
{

}
