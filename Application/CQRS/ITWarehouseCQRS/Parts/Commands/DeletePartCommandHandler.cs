using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Parts.Commands;
public class DeletePartCommandHandler : IRequestHandler<DeletePartCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    public DeletePartCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<int> Handle(DeletePartCommand request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Parts.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Parts.Remove(result);
        await _appDbContext.SaveChangesAsync();
        return result.Id;
    }
}
