using BackOfficeApp_Domain.Common;

namespace Domain.Entities.ITWarehouse;

public class Part : AuditableEntity
{
    public string Name { get; set; }
    public Category Category { get; set; }
    public Vendor Vendor{ get; set; }
    public string? Description { get; set; }
    public string? Photo { get; set; }
    public int WarrantyPeriod { get; set; }
    public bool isCurrentlyOrderable { get; set; }
    public DateTime? EndOfSupport { get; set; }
}