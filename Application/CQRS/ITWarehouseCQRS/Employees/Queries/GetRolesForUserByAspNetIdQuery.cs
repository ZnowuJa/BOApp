using MediatR;

namespace Application.ITWarehouseCQRS.Employees.Queries;

public class GetRolesForUserByAspNetIdQuery : IRequest<List<string>>
{
    public GetRolesForUserByAspNetIdQuery(string i)
    {
        EmployeeId = i;
    }
    public string EmployeeId { get; set; }
}