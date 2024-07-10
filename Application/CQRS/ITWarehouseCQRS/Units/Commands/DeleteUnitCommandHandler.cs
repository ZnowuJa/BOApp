using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Units.Commands;
public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteUnitCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {

        var item = await _appDbContext.Units.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Units.Remove(item);
        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}
