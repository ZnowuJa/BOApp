using MediatR;

namespace Application.ITWarehouseCQRS.Notes.Commands;
public class UpdateNoteCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Text { get; set; }
    public UpdateNoteCommand(int id, string text)
    {
        Id = id;
        Text = text;
    }
}
