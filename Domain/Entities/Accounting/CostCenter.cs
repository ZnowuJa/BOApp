using BackOfficeApp_Domain.Common;

namespace Domain.Entities.Accounting
{
    public class CostCenter : SmallAuditableEntity
    {
        public string MPK { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
    }
}
