using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BackOfficeApp_Domain.Common;

namespace Domain.Entities.Accounting;
public class SapCostCenter : SmallAuditableEntity
{
    public string? LocationNumber { get; set; } = string.Empty;
    public string? LocationName { get; set; } = string.Empty;
    public string? DepartmentNumber { get; set; } = string.Empty;
    public string? DepartmentName { get; set; } = string.Empty;
    public string? ResponsibleManagerName { get; set; } = string.Empty;
    public int? ResponsibleManagerEnovaId { get; set; }
    public int? ResponsibleManagerSSFId { get; set; }

}
