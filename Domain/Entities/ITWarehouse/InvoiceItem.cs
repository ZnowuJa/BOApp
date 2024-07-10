using BackOfficeApp_Domain.Common;

namespace Domain.Entities.ITWarehouse;
public class InvoiceItem : AuditableEntity
{
    public string Name { get; set; }
    public int PartId { get; set; }
    public decimal Qty { get; set; }
    public int UnitId { get; set; }
    public decimal UnitNetPrice { get; set; }
    public int CurrencyId { get; set; }
    public int InvoiceId { get; set; }
    public bool ItemsGenerated { get; set; }
    public bool Leasing { get; set; } = false;
    public DateTime? EndOfContract { get; set; }
}

