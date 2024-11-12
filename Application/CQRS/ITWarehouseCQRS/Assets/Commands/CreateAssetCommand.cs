using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Assets.Commands;
public class CreateAssetCommand : IRequest<int>
{
    public int PartId { get; set; }
    public int InvoiceId { get; set; }
    public int InvoiceItemId { get; set; }
    public int StateId { get; set; }
    public string AssetTagNumber { get; set; }
    public string SerialNumber { get; set; }
    public DateTime? LastSeen { get; set; }
    public int? AssigneeVmId { get; set; }
    public string? AssigneeVmType { get; set; }
    public string? AssigneeVmName { get; set; }
    public int? WarehouseId { get; set; }
    public decimal Price { get; set; }
    public int CurrencyId { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public bool Leasing { get; set; }
    public DateTime? EndOfContract { get; set; }
    public DateTime? WarrantyUntil { get; set; }
    public string? Imei { get; set; }
    public string? Mac { get; set; }
    public DateTime? EndOfSupport { get; set; }
    public string ModifiedBy { get; set; }


    public CreateAssetCommand(
        int partId,
        int invoiceId,
        int invoiceItemId,
        string assetTagNumber,
        int stateId,
        string serialNumber,
        DateTime? lastSeen,
        int? assigneeVmId,
        string? assigneeVmType,
        string? assigneeVmName,
        int? warehouseId,
        decimal price,
        int currencyId,
        DateTime? purchaseDate,
        bool leasing,
        DateTime? endOfContract,
        DateTime? warrantyUntil,
        string? imei,
        string? mac,
        DateTime? endOfSupport,
        string? modifiedBy
        )
    {
        PartId = partId;
        InvoiceId = invoiceId;
        InvoiceItemId = invoiceItemId;
        StateId = stateId;
        AssetTagNumber = assetTagNumber;
        SerialNumber = serialNumber;
        LastSeen = lastSeen;
        AssigneeVmId = assigneeVmId;
        AssigneeVmType = assigneeVmType;
        AssigneeVmName = assigneeVmName;
        WarehouseId = warehouseId;
        Price = price;
        CurrencyId = currencyId;
        PurchaseDate = purchaseDate;
        Leasing = leasing;
        EndOfContract = endOfContract;
        WarrantyUntil = warrantyUntil;
        Imei = imei;
        Mac = mac;
        EndOfSupport = endOfSupport;
        ModifiedBy = modifiedBy;

    }
}

public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, int>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public CreateAssetCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
    {
        Asset Asset = new()
        {
            PartId = request.PartId,
            InvoiceId = request.InvoiceId,
            InvoiceItemId = request.InvoiceItemId,
            StateId = request.StateId,
            AssetTagNumber = request.AssetTagNumber,
            SerialNumber = request.SerialNumber,
            LastSeen = request.LastSeen,
            WarehouseId = 19,
            Price = request.Price,
            CurrencyId = request.CurrencyId,
            PurchaseDate = request.PurchaseDate,
            AssigneeId = 115,
            AssigneeName = "WHIT",
            AssigneeType = "DepartmentVm",
            Leasing = request.Leasing,
            EndOfContract = request.EndOfContract,
            WarrantyUntil = request.WarrantyUntil,
            Imei = request.Imei,
            Mac = request.Mac,
            EndOfSupport = request.EndOfSupport,

        };
        _appDbContext.Assets.Add(Asset);
        await _appDbContext.SaveChangesAsync();
        await AddHistory(Asset, request.ModifiedBy);
        return Asset.Id;
    }

    private async Task AddHistory(Asset a, string? mod)
    {
        var part = await _appDbContext.Parts.Where(p => p.Id == a.PartId)
            .Include(i => i.Vendor)
            .Include(i => i.Category)
            .FirstOrDefaultAsync();

        var ah = new AssetHistory();
        ah.AssetId = a.Id.ToString();
        ah.AssetTagNumber = a.AssetTagNumber;
        ah.Serial = a.SerialNumber;
        ah.CategoryName = part.Category.Name;
        ah.PartName = part.Name;
        ah.ChangeDate = DateTime.Now;
        ah.AStateName = string.Empty;
        ah.ALongName = string.Empty;
        ah.ATypeName = string.Empty;
        ah.AWarehouseName = string.Empty;
        ah.BStateName = "New";
        ah.BLongName = a.AssigneeName;
        ah.BTypeName = a.AssigneeType;
        ah.BWarehouseName = "WHIT";
        ah.ModifiedBy = mod;
        _appDbContext.AssetsHistory.Add(ah);
        await _appDbContext.SaveChangesAsync();
    }
}