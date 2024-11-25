using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.ITWarehouseCQRS.Units.Queries;
public class GetAllUnitsForSelectQuery : IRequest<IQueryable<UnitVm>>
{
}
public class GetAllUnitsForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllUnitsForSelectQueryHandler> logger) : IRequestHandler<GetAllUnitsForSelectQuery, IQueryable<UnitVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<UnitVm>> Handle(GetAllUnitsForSelectQuery request, CancellationToken cancellationToken)
    {
        List<Domain.Entities.ITWarehouse.Unit> itemsSelected = new();
        Domain.Entities.ITWarehouse.Unit itemFirst = new Domain.Entities.ITWarehouse.Unit() { Id = 0, Name = "Select..:", ShortName = "with shortname..." };
        itemsSelected.Add(itemFirst);
        var itemsFromDb = await _appDbContext.Units.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        itemsSelected.AddRange(itemsFromDb);
        var itemsList = _mapper.Map<List<UnitVm>>(itemsSelected);

        return itemsList.AsQueryable();
    }
}
