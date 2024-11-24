namespace Application.DTOs;
public class AssetMinimal
{
    public int Id { get; set; }
    public string PartVmName { get; set; }
    public int PartVmId { get; set; }
    public string InvoiceVmNumber { get; set; }
    public int InvoiceVmId { get; set; }
    public int InvoiceItemID { get; set; }
    public string StateVmName { get; set; }
    public int StateVmId { get; set; }
    public string AssetTagNumber { get; set; }
    public string SerialNumber { get; set; }
    public DateTime? LastSeen { get; set; }
    public int? AssigneeVmId { get; set; }
    public string? AssigneeVmName { get; set; }
    public string? AssigneeVmType { get; set; }
    public string? WarehouseVmName { get; set; }
    public string? WarehouseVmNumber { get; set; }
    public int? WarehouseVmId { get; set; }
    public decimal Price { get; set; }
    public string CurrencyVmName { get; set; }
    public int CurrencyVmId { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public bool Leasing { get; set; }
    public int StatusId { get; set; }
    public DateTime? EndOfContract { get; set; }
    public DateTime? WarrantyUntil { get; set; }
    public string? Imei { get; set; }
    public string? Mac { get; set; }
    public DateTime? EndOfSupport { get; set; }
    public int? ScrappingFormId { get; set; }
    public string? ScrappingFormNumber { get; set; }
    public string? ScrappingReason { get; set; }
    public int? SaleFormId { get; set; }
    public string? SaleFormNumber { get; set; }

}
