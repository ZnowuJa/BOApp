using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Units.Commands;
public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public UpdateUnitCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<int> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var item = await _appDbContext.Units.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        item.Name = request.Name;
        await _appDbContext.SaveChangesAsync();
        return item.Id;

    }
}