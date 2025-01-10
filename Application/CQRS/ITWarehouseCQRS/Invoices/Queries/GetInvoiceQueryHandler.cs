using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Invoices.Queries;
public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, InvoiceVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetInvoiceQueryHandler(IAppDbContext appDbContext, IMapper mappper)
    {
        _appDbContext = appDbContext;
        _mapper = mappper;
    }
    public async Task<InvoiceVm> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Invoices.Where(p => p.Id == request.InvoiceId).FirstOrDefaultAsync(cancellationToken);
        var resCompanyVm = _mapper.Map<CompanyVm>(await _appDbContext.Companies.Where(p => p.Id == result.CompanyId).FirstOrDefaultAsync());
        var resCurrencyVm = _mapper.Map<CurrencyVm>(await _appDbContext.Currencies.Where(p => p.Id == result.CurrencyId).FirstOrDefaultAsync());
        var invoiceItems = new List<InvoiceItem>();
        try
        {
            invoiceItems = await _appDbContext.InvoiceItems.Where(q => q.InvoiceId == request.InvoiceId && q.StatusId == 1).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        var invoiceItemsVm = new List<InvoiceItemVm>();

        // Load all related entities in advance
        var parts = await _appDbContext.Parts.Include(i => i.Vendor).Include(i => i.Category).ToListAsync(cancellationToken);
        var units = await _appDbContext.Units.ToListAsync(cancellationToken);
        var currencies = await _appDbContext.Currencies.ToListAsync(cancellationToken);

        if (invoiceItems.Count > 0)
        {
            foreach (var item in invoiceItems)
            {
                var part = parts.FirstOrDefault(p => p.Id == item.PartId);
                var unit = units.FirstOrDefault(p => p.Id == item.UnitId);
                var currency = currencies.FirstOrDefault(p => p.Id == item.CurrencyId);

                var partVm = _mapper.Map<PartVm>(part);
                var unitVm = _mapper.Map<UnitVm>(unit);
                var currencyVm = _mapper.Map<CurrencyVm>(currency);

                var resInvItemVm = new InvoiceItemVm()
                {
                    Id = item.Id,
                    Name = item.Name,
                    PartVm = partVm,
                    PartVmName = partVm.Name,
                    PartVmId = item.PartId,
                    Qty = item.Qty,
                    UnitNetPrice = item.UnitNetPrice,
                    UnitVmId = unitVm?.Id ?? 0,
                    UnitVmName = unitVm?.Name ?? string.Empty,
                    UnitVm = unitVm,
                    CurrencyVmId = currencyVm?.Id ?? 0,
                    CurrencyVmName = currencyVm?.Name ?? string.Empty,
                    CurrencyVm = currencyVm,
                    InvoiceVmId = item.InvoiceId,
                    ItemsGenerated = item.ItemsGenerated
                };
                invoiceItemsVm.Add(resInvItemVm);
            }
        }

        //var invoiceItemsVm = new List<InvoiceItemVm>();
        //if(invoiceItems.Count > 0)
        //{
        //    foreach (var item in invoiceItems)
        //    {
        //        var partVm = _mapper.Map<PartVm>(await _appDbContext.Parts.Where(p => p.Id == item.PartId).Include(i => i.Vendor).Include(i => i.Category).FirstOrDefaultAsync());
        //        var unitVm = _mapper.Map<UnitVm>(await _appDbContext.Units.Where(p => p.Id == item.UnitId).FirstOrDefaultAsync());
        //        var currencyVm = _mapper.Map<currencyVm>(await _appDbContext.Currencies.Where(p => p.Id == item.CurrencyId).FirstOrDefaultAsync());


        //        var resInvItemVm = new InvoiceItemVm()
        //        {
        //            Id = item.Id,
        //            Title = item.Title,
        //            PartVm = partVm,
        //            PartVmName = partVm.Title,
        //            PartVmId = item.PartId,
        //            Qty = item.Qty,
        //            UnitNetPrice = item.UnitNetPrice,
        //            UnitVmId = unitVm.Id,
        //            UnitVmName = unitVm.Title,
        //            UnitVm = unitVm,
        //            CurrencyVmId = currencyVm.Id,
        //            CurrencyVmName = currencyVm.Title,
        //            currencyVm = currencyVm,
        //            InvoiceVmId = item.InvoiceId,
        //            ItemsGenerated = item.ItemsGenerated

        //        };
        //        invoiceItemsVm.Add(resInvItemVm);
        //    }
        //}

        var resVm = new InvoiceVm()
        {
            Id = result.Id,
            Number = result.Number,
            CompanyVmId = resCompanyVm.Id,
            CompanyVmName = resCompanyVm.Name,
            CompanyVm = resCompanyVm,
            Date = result.Date,
            TotalNet = result.TotalNet,
            CurrencyVmId = resCurrencyVm.Id,
            CurrencyVmName = resCurrencyVm.Name,
            CurrencyVm = resCurrencyVm,
            InvoiceItems = invoiceItemsVm,
        };
        
        return resVm;
    }
}
