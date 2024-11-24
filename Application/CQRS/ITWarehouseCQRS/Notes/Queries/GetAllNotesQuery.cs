using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Notes.Queries;
public class GetAllNotesQuery : IRequest<IQueryable<NoteVm>>
{
}
