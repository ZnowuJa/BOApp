using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Invoices.Queries;
public class GetAllInvoiceItemsQueryHandler : IRequestHandler<GetAllInvoiceItemsQuery, IQueryable<InvoiceItemVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    public GetAllInvoiceItemsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<IQueryable<InvoiceItemVm>> Handle(GetAllInvoiceItemsQuery request, CancellationToken cancellationToken)
    {
        var listItem = new List<InvoiceItemVm>();

        // Load all related entities in advance
        var parts = await _appDbContext.Parts.Include(i => i.Vendor).Include(i => i.Category).ToListAsync(cancellationToken);
        var units = await _appDbContext.Units.ToListAsync(cancellationToken);
        var currencies = await _appDbContext.Currencies.ToListAsync(cancellationToken);

        var results = await _appDbContext.InvoiceItems.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);

        foreach (var result in results)
        {
            var part = parts.FirstOrDefault(p => p.Id == result.PartId);
            var unit = units.FirstOrDefault(p => p.Id == result.UnitId);
            var currency = currencies.FirstOrDefault(p => p.Id == result.CurrencyId);

            var partVm = _mapper.Map<PartVm>(part);
            var unitVm = _mapper.Map<UnitVm>(unit);
            var currencyVm = _mapper.Map<CurrencyVm>(currency);

            var resVm = new InvoiceItemVm()
            {
                Id = result.Id,
                Name = result.Name,
                PartVm = partVm,
                PartVmName = partVm.Name,
                PartVmId = result.PartId,
                Qty = result.Qty,
                UnitNetPrice = result.UnitNetPrice,
                UnitVmId = unitVm?.Id ?? 0,
                UnitVmName = unitVm?.Name ?? string.Empty,
                UnitVm = unitVm,
                CurrencyVmId = currencyVm?.Id ?? 0,
                CurrencyVmName = currencyVm?.Name ?? string.Empty,
                CurrencyVm = currencyVm,
                InvoiceVmId = result.InvoiceId,
                ItemsGenerated = result.ItemsGenerated,
                Leasing = result.Leasing,
                EndOfContract = result.EndOfContract,
            };
            listItem.Add(resVm);
        }

        return listItem.AsQueryable();
    }
}
