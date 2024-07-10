using BackOfficeApp_Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ITWarehouse;
public class Department : SmallAuditableEntity, IAssignee
{
    public string DeptNumber { get; set; }
    public string LongName { get; set; }
    public int CompanyId { get; set; }
    public string CostCenter { get; set; }
    public int WarehouseId { get; set; }
    public int ManagerEmpId { get; set; }
    public string SapNumber { get; set; }
    //public List<Employee> EmpList {get; set;}

}
