using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Warehouses.Commands;
public class DeleteWarehouseCommand(int id) : IRequest<int>
{
    public int Id { get; set; } = id;
}
public class DeleteWarehouseCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteWarehouseCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

    public async Task<int> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {

        var ct = await _appDbContext.Warehouses.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Warehouses.Remove(ct);
        await _appDbContext.SaveChangesAsync();
        //_logger.LogInformation("DeleteCTHandler : " + ct.Id);
        return ct.Id;
    }
}