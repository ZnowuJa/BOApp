using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Notes.Queries;
public class GetAllNotesForSelectQuery : IRequest<IQueryable<NoteVm>>
{
}
