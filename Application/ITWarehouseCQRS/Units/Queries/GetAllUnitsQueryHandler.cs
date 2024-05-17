using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.ITWarehouseCQRS.Units.Queries;
public class GetAllUnitsQueryHandler : IRequestHandler<GetAllUnitsQuery, IQueryable<UnitVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllUnitsQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllUnitsQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<UnitVm>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
    {
        var curs = await _appDbContext.Units.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var curslist = _mapper.Map<List<UnitVm>>(curs);

        return curslist.AsQueryable();
    }

}
