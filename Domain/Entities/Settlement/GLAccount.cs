using BackOfficeApp_Domain.Common;

namespace Domain.Entities.Settlement
{
    public class GLAccount : SmallAuditableEntity
    {
        public string AccountNumber { get; set; }
        public string Description { get; set; }
    }
}
