
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.InvoiceItems.Queries;
public class GetInvoiceItemQueryHandler : IRequestHandler<GetInvoiceItemQuery, InvoiceItemVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetInvoiceItemQueryHandler(IAppDbContext appDbContext, IMapper mappper)
    {
        _appDbContext = appDbContext;
        _mapper = mappper;
    }
    public async Task<InvoiceItemVm> Handle(GetInvoiceItemQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.InvoiceItems.Where(p => p.Id == request.InvoiceItemId).FirstOrDefaultAsync();
        var partVm = _mapper.Map<PartVm>(await _appDbContext.Parts.Where(p => p.Id == result.PartId).Include(i => i.Vendor).Include(i => i.Category).FirstOrDefaultAsync());
        var unitVm = _mapper.Map<UnitVm>(await _appDbContext.Units.Where(p => p.Id == result.UnitId).FirstOrDefaultAsync());
        var currencyVm = _mapper.Map<CurrencyVm>(await _appDbContext.Currencies.Where(p =>p.Id == result.CurrencyId).FirstOrDefaultAsync());
        var assets = new List<Asset>();
        
        try
        {
            assets = await _appDbContext.Assets.Where(p => p.InvoiceItemId == request.InvoiceItemId).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        var assetsVm = new List<AssetVm>();
        if (assets.Count > 0)
        {
            foreach (var asset in assets)
            {
                var assetVm = _mapper.Map<AssetVm>(asset);
                assetsVm.Add(assetVm);
            }
        }


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
            AssetsVm = assetsVm,
            Leasing = result.Leasing,
            EndOfContract = result.EndOfContract,

        };
        
        return resVm;
    }
}
