using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.General;
public class OrganisationRoleForFormVm
{
    public bool IsDefault { get; set; } = false;
    public int EmpId { get; set; } = 0;
    public string LongName { get; set; } = string.Empty;


    public OrganisationRoleForFormVm()
    {
        
    }

    public OrganisationRoleForFormVm(OrganisationRoleVm role)
    {
        IsDefault = role.IsDefault;
        EmpId = role.EmpId;
        LongName = role.Employee.LongName;
    }
}
