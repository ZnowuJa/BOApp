using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.ITWarehouseCQRS.Currencies.Queries;
public class GetAllCurrenciesQueryHandler : IRequestHandler<GetAllCurrenciesQuery, IQueryable<CurrencyVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllCurrenciesQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllCurrenciesQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<CurrencyVm>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var curs =  await _appDbContext.Currencies.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var curslist = _mapper.Map<List<CurrencyVm>>(curs);

        return curslist.AsQueryable();
    }

}
