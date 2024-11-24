using MediatR;

namespace Application.ITWarehouseCQRS.Employees.Commands;
public class DeleteEmployeeCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteEmployeeCommand(int id)
    {
        Id = id;
    }
}
