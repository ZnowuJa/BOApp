using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Warehouses.Queries;
public class GetWarehouseByNumberQuery(string n) : IRequest<WarehouseVm>
{
    public string WarehouseNumber { get; set; } = n;
}
public class GetWarehouseByNumberQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetWarehouseByNumberQuery, WarehouseVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<WarehouseVm> Handle(GetWarehouseByNumberQuery request, CancellationToken cancellationToken)

    {
        var item = await _appDbContext.Warehouses.Where(p => p.Number.ToString() == request.WarehouseNumber).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var itemVm = _mapper.Map<WarehouseVm>(item);
        return itemVm;
    }
}