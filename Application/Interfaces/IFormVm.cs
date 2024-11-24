using Application.ViewModels.General;

namespace Application.Interfaces;
public interface IFormVm
{
    string Status { get; set; }
    string LVL1_EnovaEmpId { get; set; }
    string LVL1_EmployeeName { get; set; }
    string LVL2_EnovaEmpId { get; set; }
    string LVL2_EmployeeName { get; set; }
    List<OrganisationRoleForFormVm> Level1Approvers { get; set; }
    List<OrganisationRoleForFormVm> Level2Approvers { get; set; }
}
