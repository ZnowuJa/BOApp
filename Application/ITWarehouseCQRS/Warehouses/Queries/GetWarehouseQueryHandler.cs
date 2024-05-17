using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Warehouses.Queries;
public class GetWarehouseQueryHandler : IRequestHandler<GetWarehouseQuery, WarehouseVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetWarehouseQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<WarehouseVm> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
    {
        var item = await _appDbContext.Warehouses.Where(p => p.Id == request.WarehouseId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var itemVm = _mapper.Map<WarehouseVm>(item);
        return itemVm;
    }
}
