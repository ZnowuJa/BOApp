using BackOfficeApp_Domain.Common;

namespace Domain.Entities.ITWarehouse;

public class Unit : SmallAuditableEntity
{
    public string Name { get; set; }
    public string ShortName { get; set; }

}


