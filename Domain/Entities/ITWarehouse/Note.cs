using BackOfficeApp_Domain.Common;

namespace Domain.Entities.ITWarehouse;

public class Note : AuditableEntity
{
    public string Text {get; set;}

}

