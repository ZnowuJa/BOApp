using BackOfficeApp_Domain.Common;

namespace Domain.Entities.ITWarehouse;

public class Invoice : AuditableEntity
{
    public string Number { get; set; }
    public int CompanyId { get; set; }   
    public DateTime? Date {get; set;}
    public decimal TotalNet { get; set; }
    public int CurrencyId { get; set; }
}