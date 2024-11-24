using MediatR;

namespace Application.ITWarehouseCQRS.Units.Commands;
public class DeleteUnitCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteUnitCommand(int id)
    {
        Id = id;
    }
}
