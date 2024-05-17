using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.AssetsById.Commands;
public class DeleteAssetByIdCommandHandler : IRequestHandler<DeleteAssetByIdCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    public DeleteAssetByIdCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Handle(DeleteAssetByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Assets.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Assets.Remove(result);
        await _appDbContext.SaveChangesAsync();
        return result.Id;
    }
}
