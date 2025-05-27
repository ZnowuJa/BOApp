using MediatR;

namespace Application.ITWarehouseCQRS.Notes.Commands;
public class DeleteNoteCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteNoteCommand(int id)
    {
        Id = id;
    }
}
