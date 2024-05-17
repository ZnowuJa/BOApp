using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Warehouses.Commands;
public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteWarehouseCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {

        var ct = await _appDbContext.Warehouses.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Warehouses.Remove(ct);
        await _appDbContext.SaveChangesAsync();
        //_logger.LogInformation("DeleteCTHandler : " + ct.Id);
        return ct.Id;
    }
}
