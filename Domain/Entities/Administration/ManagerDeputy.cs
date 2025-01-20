using BackOfficeApp_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Administration
{
    public class ManagerDeputy : SmallAuditableEntity
    {
        public int ManagerId { get; set; }
        public string LongName { get; set; }
        public string Deputies { get; set; } = "[]";
    }
}
