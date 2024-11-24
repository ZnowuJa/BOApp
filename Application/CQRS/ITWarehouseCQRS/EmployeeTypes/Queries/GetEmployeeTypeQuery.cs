using Application.ViewModels.General;
using MediatR;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Queries;
public class GetEmployeeTypeQuery : IRequest<EmployeeTypeVm>
{
    public GetEmployeeTypeQuery(int i)
    {
        EmployeeTypeId = i;
    }

    public int EmployeeTypeId { get; set; }
}
