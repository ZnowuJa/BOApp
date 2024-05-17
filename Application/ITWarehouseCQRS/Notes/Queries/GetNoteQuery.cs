using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Notes.Queries;
public class GetNoteQuery : IRequest<NoteVm>
{
    public GetNoteQuery(int i)
    {
        NoteId = i;
    }
    public int NoteId { get; set; }
}
