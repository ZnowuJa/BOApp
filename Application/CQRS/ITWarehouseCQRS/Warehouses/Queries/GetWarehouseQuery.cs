using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Warehouses.Queries;
public class GetWarehouseQuery(int i) : IRequest<WarehouseVm>
{
    public int WarehouseId { get; set; } = i;
}
public class GetWarehouseQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetWarehouseQuery, WarehouseVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<WarehouseVm> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
    {
        var item = await _appDbContext.Warehouses.Where(p => p.Id == request.WarehouseId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var itemVm = _mapper.Map<WarehouseVm>(item);
        return itemVm;
    }
}