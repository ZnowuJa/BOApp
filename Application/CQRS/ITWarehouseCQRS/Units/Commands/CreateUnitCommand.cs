using Application.Interfaces;
using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.Units.Commands;
public class CreateUnitCommand(string name, string shortName) : IRequest<int>
{
    public string Name { get; set; } = name;
    public string ShortName { get; set; } = shortName;
}
public class CreateUnitCommandHandler(IAppDbContext appDbContext) : IRequestHandler<CreateUnitCommand, int>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

    public async Task<int> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.ITWarehouse.Unit item = new()
        {
            Name = request.Name,
            ShortName = request.ShortName,
            StatusId = 1
        };
        _appDbContext.Units.Add(item);
        await _appDbContext.SaveChangesAsync();

        return item.Id;
    }
}