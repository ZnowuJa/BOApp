
using Application.ViewModels.General;

namespace Application.Interfaces;
public interface IFormAccounting
{
    int Id { get; set; }
    string Status { get; set; }
    string EnovaEmpId { get; set; }
    //string EnovaEmpId { get; set; }
    string LVL1_EnovaEmpId { get; set; }
    string LVL1_EmployeeName { get; set; }
    string LVL2_EnovaEmpId { get; set; }
    string LVL2_EmployeeName { get; set; }
    List<OrganisationRoleForFormVm> Level1Approvers { get; set; }
    List<OrganisationRoleForFormVm> Level2Approvers { get; set; }
    List<FormFileVm> FormFiles { get; set; }
    string NumberPrefix { get; set; }
    string FolderName { get; set; }
}

