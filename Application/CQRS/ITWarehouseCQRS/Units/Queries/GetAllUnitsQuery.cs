using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.ITWarehouseCQRS.Units.Queries;
public class GetAllUnitsQuery : IRequest<IQueryable<UnitVm>>
{
}
public class GetAllUnitsQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllUnitsQueryHandler> logger) : IRequestHandler<GetAllUnitsQuery, IQueryable<UnitVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<UnitVm>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
    {
        var curs = await _appDbContext.Units.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var curslist = _mapper.Map<List<UnitVm>>(curs);

        return curslist.AsQueryable();
    }
}