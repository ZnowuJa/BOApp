using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Units.Commands;
public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, int>
{
    private readonly IAppDbContext _appDbContext;

    public CreateUnitCommandHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

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
