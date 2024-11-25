using MediatR;
using Application.ViewModels;
using Application.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Currencies.Queries;
public class GetAllCurrenciesQuery : IRequest<IQueryable<CurrencyVm>>
{
}
public class GetAllCurrenciesQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllCurrenciesQueryHandler> logger) : IRequestHandler<GetAllCurrenciesQuery, IQueryable<CurrencyVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<CurrencyVm>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var curs = await _appDbContext.Currencies.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var curslist = _mapper.Map<List<CurrencyVm>>(curs);

        return curslist.AsQueryable();
    }
}