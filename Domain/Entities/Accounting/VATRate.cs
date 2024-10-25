using BackOfficeApp_Domain.Common;

namespace Domain.Entities.Accounting
{
    public class VATRate : SmallAuditableEntity
    {
        public string Name { get; set; }
        public double Percentage { get; set; }
        public string Information { get; set; }
        public int Order { get; set; }
    }
}
