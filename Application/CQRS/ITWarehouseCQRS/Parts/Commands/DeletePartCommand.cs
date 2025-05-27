using MediatR;

namespace Application.ITWarehouseCQRS.Parts.Commands;
public class DeletePartCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeletePartCommand(int id)
    {
        Id = id;
    }
}
