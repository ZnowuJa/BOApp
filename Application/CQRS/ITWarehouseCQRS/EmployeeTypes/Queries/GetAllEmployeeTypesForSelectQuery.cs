using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.EmployeeTypes.Queries;
public class GetAllEmployeeTypesForSelectQuery : IRequest<IQueryable<EmployeeTypeVm>>
{
}
