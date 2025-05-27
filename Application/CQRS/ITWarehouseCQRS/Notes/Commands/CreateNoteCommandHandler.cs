using Application.Interfaces;
using Domain.Entities.ITWarehouse;
using MediatR;

namespace Application.ITWarehouseCQRS.Notes.Commands;
public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public CreateNoteCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        Note note = new()
        {
            Text = request.Text,
            StatusId = 1
        };
        _appDbContext.Notes.Add(note);
        await _appDbContext.SaveChangesAsync();

        return note.Id;
    }
}
