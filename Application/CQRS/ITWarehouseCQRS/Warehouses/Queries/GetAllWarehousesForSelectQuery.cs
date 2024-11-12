using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace Application.CQRS.ITWarehouseCQRS.Warehouses.Queries;
public class GetAllWarehousesForSelectQuery : IRequest<IQueryable<WarehouseVm>>
{
}
public class GetAllWarehousesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllWarehousesForSelectQueryHandler> logger) : IRequestHandler<GetAllWarehousesForSelectQuery, IQueryable<WarehouseVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<WarehouseVm>> Handle(GetAllWarehousesForSelectQuery request, CancellationToken cancellationToken)
    {
        List<Warehouse> itemsSelected = new();
        Warehouse itemFirst = new Warehouse() { Id = 0, Number = 9999, Name = "Select..." };
        itemsSelected.Add(itemFirst);
        var itemsFromDb = await _appDbContext.Warehouses.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        itemsSelected.AddRange(itemsFromDb);
        var itemsList = _mapper.Map<List<WarehouseVm>>(itemsSelected);

        return itemsList.AsQueryable();
    }
}