using MediatR;
using Application.ViewModels;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Currencies.Queries;
public class GetAllCurrenciesForSelectQuery : IRequest<IQueryable<CurrencyVm>>
{
}
public class GetAllCurrenciesForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllCurrenciesForSelectQueryHandler> logger) : IRequestHandler<GetAllCurrenciesForSelectQuery, IQueryable<CurrencyVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<CurrencyVm>> Handle(GetAllCurrenciesForSelectQuery request, CancellationToken cancellationToken)
    {
        List<Currency> itemsSelected = new();
        Currency itemFirst = new Currency() { Id = 0, Name = "Select..." };
        itemsSelected.Add(itemFirst);
        var itemsFromDb = await _appDbContext.Currencies.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        itemsSelected.AddRange(itemsFromDb);
        var itemsList = _mapper.Map<List<CurrencyVm>>(itemsSelected);

        return itemsList.AsQueryable();
    }

}