using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Entities.ITWarehouse;

namespace Application.ITWarehouseCQRS.Units.Queries;
public class GetAllUnitsForSelectQueryHandler : IRequestHandler<GetAllUnitsForSelectQuery, IQueryable<UnitVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllUnitsForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllUnitsForSelectQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<UnitVm>> Handle(GetAllUnitsForSelectQuery request, CancellationToken cancellationToken)
    {
        List<Domain.Entities.ITWarehouse.Unit> itemsSelected = new();
        Domain.Entities.ITWarehouse.Unit itemFirst = new Domain.Entities.ITWarehouse.Unit(){ Id = 0, Name = "Select..:", ShortName = "with shortname..."  };
        itemsSelected.Add(itemFirst);
        var itemsFromDb = await _appDbContext.Units.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        itemsSelected.AddRange(itemsFromDb);
        var itemsList = _mapper.Map<List<UnitVm>>(itemsSelected);

        return itemsList.AsQueryable();
    }
}
