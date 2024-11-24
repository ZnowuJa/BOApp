using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Notes.Commands;
public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteNoteCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

    }

    public async Task<int> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {

        var item = await _appDbContext.Notes.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Notes.Remove(item);
        await _appDbContext.SaveChangesAsync();
        //_logger.LogInformation("DeleteCTHandler : " + ct.Id);
        return item.Id;
    }
}