using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Assets.Commands;
public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    public DeleteAssetCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Assets.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Assets.Remove(result);
        await _appDbContext.SaveChangesAsync();
        return result.Id;
    }
}
