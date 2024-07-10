using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Warehouses.Queries;
public class GetAllWarehousesForSelectQueryHandler : IRequestHandler<GetAllWarehousesForSelectQuery, IQueryable<WarehouseVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllWarehousesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllWarehousesForSelectQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

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
