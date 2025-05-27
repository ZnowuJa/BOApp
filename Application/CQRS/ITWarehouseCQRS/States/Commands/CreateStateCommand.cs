using MediatR;

namespace Application.ITWarehouseCQRS.States.Commands;
public class CreateStateCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public CreateStateCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
