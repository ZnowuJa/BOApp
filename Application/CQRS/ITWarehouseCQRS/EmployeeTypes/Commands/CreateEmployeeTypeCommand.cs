using MediatR;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Commands;
public class CreateEmployeeTypeCommand : IRequest<int>
{
    public string Name { get; set; }

    public CreateEmployeeTypeCommand(string name)
    {
        Name = name;
    }
}
