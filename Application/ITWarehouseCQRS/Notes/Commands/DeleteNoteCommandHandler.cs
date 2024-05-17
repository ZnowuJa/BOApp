using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Notes.Commands;
public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteNoteCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        Console.WriteLine("Byłem tu!");
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