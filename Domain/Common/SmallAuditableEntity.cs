using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOfficeApp_Domain.Common
{
    public class SmallAuditableEntity
    {
        public int Id { get; set; }
        public int? StatusId { get; set; }
        public string? InactivatedBy { get; set; }
        public DateTime? Inactivated { get; set; }
    }
}
