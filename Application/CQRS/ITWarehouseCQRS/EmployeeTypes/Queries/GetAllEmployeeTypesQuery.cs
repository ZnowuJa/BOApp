using Application.ViewModels.General;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.EmployeeTypes.Queries;
public class GetAllEmployeeTypesQuery : IRequest<IQueryable<EmployeeTypeVm>>
{
}
