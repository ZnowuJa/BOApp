using BackOfficeApp_Domain.Common;

namespace Domain.Entities.ITWarehouse;

public class Asset_Note : AuditableEntity
{
    public Asset item { get; set; }
    public Note note { get; set; }

}
