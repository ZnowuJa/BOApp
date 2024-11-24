using MediatR;

namespace Application.ITWarehouseCQRS.Notes.Commands;
public class CreateNoteCommand : IRequest<int>
{
    public string Text { get; set; }

    public CreateNoteCommand(string text)
    {
        Text = text;
    }
}