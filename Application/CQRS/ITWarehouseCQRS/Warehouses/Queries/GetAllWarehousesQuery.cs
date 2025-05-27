using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.ITWarehouseCQRS.Warehouses.Queries;
public class GetAllWarehousesQuery : IRequest<IQueryable<WarehouseVm>>
{
}
public class GetAllWarehousesQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllWarehousesQueryHandler> logger) : IRequestHandler<GetAllWarehousesQuery, IQueryable<WarehouseVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<WarehouseVm>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
    {
        var items = await _appDbContext.Warehouses.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var itemsList = _mapper.Map<List<WarehouseVm>>(items);

        return itemsList.AsQueryable();
    }

}
