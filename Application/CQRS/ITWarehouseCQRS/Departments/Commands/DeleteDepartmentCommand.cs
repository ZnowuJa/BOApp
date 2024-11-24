using MediatR;

namespace Application.ITWarehouseCQRS.Departments.Commands;
public class DeleteDepartmentCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteDepartmentCommand(int id)
    {
        Id = id;
    }
}
