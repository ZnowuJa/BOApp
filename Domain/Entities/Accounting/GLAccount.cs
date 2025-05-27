using BackOfficeApp_Domain.Common;

namespace Domain.Entities.Accounting
{
    public class GLAccount : SmallAuditableEntity
    {
        public string AccountNumber { get; set; }
        public string Description { get; set; }
    }
}
