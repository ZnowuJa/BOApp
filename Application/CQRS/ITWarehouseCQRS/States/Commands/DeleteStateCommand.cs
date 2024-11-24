using MediatR;

namespace Application.ITWarehouseCQRS.States.Commands;
public class DeleteStateCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteStateCommand(int id)
    {
        Id = id;
    }
}
