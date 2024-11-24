using Application.ViewModels.General;
using MediatR;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Queries;
public class GetAllEmployeeTypesForSelectQuery : IRequest<IQueryable<EmployeeTypeVm>>
{
}
