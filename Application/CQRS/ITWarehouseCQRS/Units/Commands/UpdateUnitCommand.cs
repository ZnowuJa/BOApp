using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Units.Commands;
public class UpdateUnitCommand(int id, string name, string shortName) : IRequest<int>
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string ShortName { get; set; } = shortName;
}
public class UpdateUnitCommandHandler(IAppDbContext appDbContext) : IRequestHandler<UpdateUnitCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

    public async Task<int> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
    {
        // czy ze strony przekazuję ID do Update'u???
        var item = await _appDbContext.Units.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        item.Name = request.Name;
        await _appDbContext.SaveChangesAsync();
        return item.Id;
    }
}