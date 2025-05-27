using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Employees.Commands;
public class DeleteEmployeeCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteEmployeeCommand(int id)
    {
        Id = id;
    }
}
