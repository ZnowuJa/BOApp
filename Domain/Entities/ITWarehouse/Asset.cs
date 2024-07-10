using BackOfficeApp_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ITWarehouse;
public class Asset : AuditableEntity
{
    public int PartId { get; set; }
    public int InvoiceId { get; set; }
    public int InvoiceItemId { get; set; }
    public int StateId { get; set; }
    public string AssetTagNumber { get; set; }
    public string SerialNumber { get; set; }
    public DateTime? LastSeen { get; set; }
    public int? AssigneeId { get; set; }
    public string? AssigneeName { get; set; }
    public string? AssigneeType { get; set; }
    public int? WarehouseId { get; set; }
    public decimal Price { get; set; }
    public int CurrencyId { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public bool Leasing { get; set; }
    public DateTime? EndOfContract { get; set; }
    public DateTime? WarrantyUntil { get; set; }
    public string? Imei {  get; set; }
    public string? Mac {  get; set; }
    public DateTime? EndOfSupport { get; set; }

    public Asset()
    {
        
    }

    public Asset(Asset other)
    {
        Id = other.Id;
        PartId = other.PartId;
        InvoiceId = other.InvoiceId;
        InvoiceItemId = other.InvoiceItemId;
        StateId = other.StateId;
        AssetTagNumber = other.AssetTagNumber;
        SerialNumber = other.SerialNumber;
        LastSeen = other.LastSeen;
        AssigneeId = other.AssigneeId;
        AssigneeName = other.AssigneeName;
        AssigneeType = other.AssigneeType;
        WarehouseId = other.WarehouseId;
        Price = other.Price;
        CurrencyId = other.CurrencyId;
        PurchaseDate = other.PurchaseDate;
        Leasing = other.Leasing;
        StatusId = other.StatusId;
        EndOfContract = other.EndOfContract;
        WarrantyUntil = other.WarrantyUntil;
        Imei = other.Imei;
        Mac = other.Mac;
        EndOfSupport = other.EndOfSupport;
    }

}
