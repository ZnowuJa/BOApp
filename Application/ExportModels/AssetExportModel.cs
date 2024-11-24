namespace Application.ExportModels;
public class AssetExportModel
{ 
    public int Id { get; set; }
    public string AssetTagNumber { get; set; }
    public string SerialNumber { get; set; }
    public string PartVmName { get; set; }
    public string StateVmName { get; set; }
    public string InvoiceDocNumber { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public string? AssigneeVmName { get; set; }
    public string? AssigneeVmType { get; set; }
     public string? WarehouseVmName { get; set; }
    public string? WarehouseVmNumber { get; set; }
    public decimal Price { get; set; }
    public string CurrencyVmName { get; set; }
    public string? Imei { get; set; }
    public string? Mac { get; set; }

    public bool Leasing { get; set; }
    public int StatusId { get; set; }
    public DateTime? EndOfContract { get; set; }
    public DateTime? WarrantyUntil { get; set; }
    
    public DateTime? EndOfSupport { get; set; }

    public int InvoiceVmId { get; set; }
    public int PartVmId { get; set; }
    public int StateVmId { get; set; }
    public int InvoiceItemID { get; set; }

}