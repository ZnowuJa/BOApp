using BackOfficeApp_Domain.Common;

namespace Domain.Entities.ITWarehouse;

public class Warehouse : SmallAuditableEntity
{
    public int Number { get; set; }
    public string Name { get; set; }

}

