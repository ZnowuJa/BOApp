using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Domain.Entities.ITWarehouse;

namespace Application.ITWarehouseCQRS.Currencies.Queries;
public class GetAllCurrenciesForSelectQueryHandler : IRequestHandler<GetAllCurrenciesForSelectQuery, IQueryable<CurrencyVm>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetAllCurrenciesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllCurrenciesForSelectQueryHandler> logger)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IQueryable<CurrencyVm>> Handle(GetAllCurrenciesForSelectQuery request, CancellationToken cancellationToken)
    {
        List<Currency> itemsSelected = new();
        Currency itemFirst = new Currency() { Id = 0, Name = "Select..." };
        itemsSelected.Add(itemFirst);
        var itemsFromDb =  await _appDbContext.Currencies.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        itemsSelected.AddRange(itemsFromDb);
        var itemsList = _mapper.Map<List<CurrencyVm>>(itemsSelected);

        return itemsList.AsQueryable();
    }

}
