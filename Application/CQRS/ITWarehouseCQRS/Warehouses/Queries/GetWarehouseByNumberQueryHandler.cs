using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
