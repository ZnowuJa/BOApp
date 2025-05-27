using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Invoices.Queries;
public class GetAllInvoiceItemsByInvoiceIdQueryHandler : IRequestHandler<GetAllInvoiceItemsByInvoiceIdQuery, List<InvoiceItemVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    public GetAllInvoiceItemsByInvoiceIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<List<InvoiceItemVm>> Handle(GetAllInvoiceItemsByInvoiceIdQuery request, CancellationToken cancellationToken)
    {
        var listItem = new List<InvoiceItemVm>();
        var results = new List<InvoiceItem>();
        try
        {
            results = await _appDbContext.InvoiceItems.Where(p => p.StatusId == 1).Where(q => q.InvoiceId == request.InvoiceId).ToListAsync(cancellationToken);

        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }



        foreach (var result in results)
        {
            var partVm = _mapper.Map<PartVm>(await _appDbContext.Parts.Where(p => p.Id == result.PartId).Include(i => i.Vendor).Include(i => i.Category).FirstOrDefaultAsync());
            var unitVm = _mapper.Map<UnitVm>(await _appDbContext.Units.Where(p => p.Id == result.UnitId).FirstOrDefaultAsync());
            var currencyVm = _mapper.Map<CurrencyVm>(await _appDbContext.Currencies.Where(p => p.Id == result.CurrencyId).FirstOrDefaultAsync());

            var resVm = new InvoiceItemVm()
            {
                Id = result.Id,
                Name = result.Name,
                PartVm = partVm,
                PartVmName = partVm.Name,
                PartVmId = result.PartId,
                Qty = result.Qty,
                UnitNetPrice = result.UnitNetPrice,
                UnitVmId = unitVm.Id,
                UnitVmName = unitVm.Name,
                UnitVm = unitVm,
                CurrencyVmId = currencyVm.Id,
                CurrencyVmName = currencyVm.Name,
                CurrencyVm = currencyVm,
                InvoiceVmId = result.InvoiceId,
                ItemsGenerated = result.ItemsGenerated,
                Leasing = result.Leasing,
                EndOfContract = result.EndOfContract,
            };
            listItem.Add(resVm);
        }

        //var res = _mapper.Map<List<InvoiceVm>>(result);
        return listItem;
    }
}
