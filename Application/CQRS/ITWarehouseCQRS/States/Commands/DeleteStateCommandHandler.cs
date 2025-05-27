using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.States.Commands;
public class DeleteStateCommandHandler : IRequestHandler<DeleteStateCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public DeleteStateCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        Console.WriteLine("Byłem tu!");
    }

    public async Task<int> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
    {

        var item = await _appDbContext.States.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.States.Remove(item);
        await _appDbContext.SaveChangesAsync();
        //_logger.LogInformation("DeleteCTHandler : " + ct.Id);
        return item.Id;
    }
}
