using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Currencies.Queries;
public class GetCurrencyQuery(int i) : IRequest<CurrencyVm>
{
    public int CurrencyId { get; set; } = i;
}
public class GetCurrencyQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetCurrencyQuery, CurrencyVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public IMapper Mapper { get; }

    public async Task<CurrencyVm> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
    {
        var currency = await _appDbContext.Currencies.Where(p => p.Id == request.CurrencyId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var currencyVM = _mapper.Map<CurrencyVm>(currency);
        return currencyVM;
    }
}