using BackOfficeApp_Domain.Common;

namespace Domain.Entities.ITWarehouse;

public class State : SmallAuditableEntity
{ 
    public string Name { get; set; }
    public string Description { get; set; }

}
