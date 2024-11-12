using BackOfficeApp_Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Accounting
{
    public class Country : SmallAuditableEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsEU { get; set; }
    }
}
