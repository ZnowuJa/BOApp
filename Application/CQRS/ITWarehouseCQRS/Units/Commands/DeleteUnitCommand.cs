using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Units.Commands;
public class DeleteUnitCommand(int id) : IRequest<int>
{
    public int Id { get; set; } = id;
}
public class DeleteUnitCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteUnitCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

    public async Task<int> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        var item = await _appDbContext.Units.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        _appDbContext.Units.Remove(item);
        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}