using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.ITWarehouseCQRS.States.Queries;
public class GetAllStatesQueryHandler : IRequestHandler<GetAllStatesQuery, IQueryable<StateVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllStatesQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllStatesQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<StateVm>> Handle(GetAllStatesQuery request, CancellationToken cancellationToken)
    {
        var curs = await _appDbContext.States.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var curslist = _mapper.Map<List<StateVm>>(curs);

        return curslist.AsQueryable();
    }

}
