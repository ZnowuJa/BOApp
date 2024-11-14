using MediatR;

namespace Application.ITWarehouseCQRS.Assets.Commands;
public class UpdateAssetCommand : IRequest<int>
{
    public int Id { get; set; }
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
    public string? ModifiedBy { get; set; }
    public int? ScrappingFormId { get; set; }
    public int? SaleFormId { get; set; }
    public string? ScrappingReason { get; set; }



    public UpdateAssetCommand(
        int id, 
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
        int? scrappingFormId,
        int? saleFormId,
        string? scrappingReason,
        string? modifiedBy
        )
    {
        Id = id;
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
        ScrappingFormId = scrappingFormId;
        SaleFormId = saleFormId;
        ScrappingReason = scrappingReason;
    }
}