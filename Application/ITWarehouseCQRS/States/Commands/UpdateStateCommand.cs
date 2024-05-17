using MediatR;

namespace Application.ITWarehouseCQRS.States.Commands;
public class UpdateStateCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public UpdateStateCommand(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}
