using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.General;
public class OrganisationRoleVm
{
    public bool IsDefault { get; set; }
    public int EmpId {  get; set; }
    public EmployeeVm Employee { get; set; }

    public OrganisationRoleVm()
    {
        Employee = new EmployeeVm();
    }
}
