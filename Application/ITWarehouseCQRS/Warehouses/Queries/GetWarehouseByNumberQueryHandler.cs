using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;

using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Warehouses.Queries;
public class GetWarehouseByNumberQueryHandler : IRequestHandler<GetWarehouseByNumberQuery, WarehouseVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetWarehouseByNumberQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<WarehouseVm> Handle(GetWarehouseByNumberQuery request, CancellationToken cancellationToken)

    {
    var item = await _appDbContext.Warehouses.Where(p => p.Number.ToString() == request.WarehouseNumber).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    var itemVm = _mapper.Map<WarehouseVm>(item);
    return itemVm;
    }
}
