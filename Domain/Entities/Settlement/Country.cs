using BackOfficeApp_Domain.Common;

namespace Domain.Entities.Settlement
{
    public class Country : SmallAuditableEntity
    {
            public string Code { get; set; }
            public string Name { get; set; }
            public bool IsEU { get; set; }
    }
}
