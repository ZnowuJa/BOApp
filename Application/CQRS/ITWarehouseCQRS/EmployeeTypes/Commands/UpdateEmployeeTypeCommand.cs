using MediatR;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Commands;
public class UpdateEmployeeTypeCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UpdateEmployeeTypeCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
