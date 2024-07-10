using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.ITWarehouse;

namespace Application.ITWarehouseCQRS.Currencies.Queries;
public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, CurrencyVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetCurrencyQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<CurrencyVm> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
    {
        var currency = await _appDbContext.Currencies.Where(p => p.Id == request.CurrencyId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var currencyVM = _mapper.Map<CurrencyVm>(currency);
        return currencyVM;
    }
}
