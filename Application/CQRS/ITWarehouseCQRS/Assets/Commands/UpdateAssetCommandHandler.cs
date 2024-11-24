using Application.Interfaces;
using AutoMapper;
using Domain.Entities.ITWarehouse;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Assets.Commands;
public class UpdateAssetCommandHandler : IRequestHandler<UpdateAssetCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public UpdateAssetCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<int> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
    {
        var itemB = await _appDbContext.Assets.Where(p => p.Id == request.Id).FirstOrDefaultAsync();
        var itemA = new Asset(itemB);

        itemB.PartId = request.PartId;
        itemB.InvoiceId = request.InvoiceId;
        itemB.InvoiceItemId = request.InvoiceItemId;
        itemB.StateId = request.StateId;
        itemB.AssetTagNumber = request.AssetTagNumber;
        itemB.SerialNumber = request.SerialNumber;
        itemB.LastSeen = request.LastSeen;
        itemB.AssigneeId = request.AssigneeVmId;
        itemB.AssigneeName = request.AssigneeVmName;
        itemB.AssigneeType = request.AssigneeVmType;
        itemB.WarehouseId = request.WarehouseId;
        itemB.Price = request.Price;
        itemB.CurrencyId = request.CurrencyId;
        itemB.PurchaseDate = request.PurchaseDate;
        itemB.Leasing = request.Leasing;
        itemB.EndOfContract = request.EndOfContract;
        itemB.WarrantyUntil = request.WarrantyUntil;
        itemB.Imei = request.Imei;
        itemB.Mac = request.Mac;
        itemB.EndOfSupport = request.EndOfSupport;
        itemB.ScrappingFormId = request.ScrappingFormId;
        itemB.SaleFormId = request.SaleFormId;
        itemB.ScrappingReason = request.ScrappingReason;


        _appDbContext.Assets.Update(itemB);

        await _appDbContext.SaveChangesAsync();
        await AddHistory(itemA, itemB, request.ModifiedBy);
        return itemB.Id;
    }
    
    private async Task AddHistory(Asset a, Asset b, string? mod)
    {
        var whA = await _appDbContext.Warehouses.Where(w => w.Id == a.WarehouseId).FirstOrDefaultAsync();
        var whB = await _appDbContext.Warehouses.Where(w => w.Id == b.WarehouseId).FirstOrDefaultAsync();
        var stateA = await _appDbContext.States.Where(s => s.Id == a.StateId).FirstOrDefaultAsync();
        var stateB = await _appDbContext.States.Where(s => s.Id == b.StateId).FirstOrDefaultAsync();
        var partA = await _appDbContext.Parts.Where(p => p.Id == a.PartId)
            .Include(i => i.Vendor)
            .Include(i => i.Category)
            .FirstOrDefaultAsync();
        var partB = await _appDbContext.Parts.Where(p => p.Id == b.PartId)
            .Include(i => i.Vendor)
            .Include(i => i.Category)
            .FirstOrDefaultAsync();

        var ah = new AssetHistory();
        ah.AssetId = a.Id.ToString();
        ah.AssetTagNumber = a.AssetTagNumber;
        ah.Serial = a.SerialNumber;
        ah.CategoryName = partA.Category.Name;
        ah.PartName = partA.Name;
        ah.ChangeDate = DateTime.Now;
        ah.AStateName = stateA.Name;
        ah.ALongName = a.AssigneeName;
        ah.ATypeName = a.AssigneeType;
        ah.AWarehouseName = whA?.Name ?? string.Empty;
        ah.BStateName = stateB.Name;
        ah.BLongName = b.AssigneeName;
        ah.BTypeName = b.AssigneeType;
        ah.BWarehouseName = whB?.Name ?? string.Empty;
        ah.ModifiedBy = mod;
        _appDbContext.AssetsHistory.Add(ah);
        await _appDbContext.SaveChangesAsync();
    }
}
