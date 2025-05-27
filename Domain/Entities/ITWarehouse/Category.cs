
using BackOfficeApp_Domain.Common;

namespace Domain.Entities.ITWarehouse;

public class Category : SmallAuditableEntity
{
    public string Name { get; set; }
    public string Prefix { get; set; }
    public int LeadingZeros { get; set; }
    public CategoryType CategoryType { get; set; }

}
