using MediatR;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Application.CQRS.ITWarehouseCQRS.Assets.Commands;
public class DeleteAssetCommand(int id) : IRequest<int>
{
    public int Id { get; set; } = id;
}
public class DeleteAssetCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteAssetCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

    public async Task<int> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Assets.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Assets.Remove(result);
        await _appDbContext.SaveChangesAsync();
        return result.Id;
    }
}