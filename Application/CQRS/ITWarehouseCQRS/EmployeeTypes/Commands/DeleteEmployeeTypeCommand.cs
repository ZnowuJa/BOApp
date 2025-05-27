using MediatR;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Commands;
public class DeleteEmployeeTypeCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteEmployeeTypeCommand(int id)
    {
        Id = id;
    }
}
