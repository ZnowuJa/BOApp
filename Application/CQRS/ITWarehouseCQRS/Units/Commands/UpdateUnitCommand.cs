using MediatR;

namespace Application.ITWarehouseCQRS.Units.Commands;
public class UpdateUnitCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public UpdateUnitCommand(int id, string name, string shortName)
    {
        Id = id;
        Name = name;
        ShortName = shortName;
    }
}
