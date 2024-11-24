using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Notes.Commands;
public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public UpdateNoteCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        
        var note = await _appDbContext.Notes.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        note.Text = request.Text;
        await _appDbContext.SaveChangesAsync();
        return note.Id;

    }
}
