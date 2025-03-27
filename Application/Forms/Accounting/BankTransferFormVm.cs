using Application.Interfaces;
using Application.ViewModels.General;

namespace Application.Forms.Accounting;

public class BankTransferFormVm : IFormAccounting
{
    public int Id { get; set; }
    public string Status { get; set; }
    public string EnovaEmpId { get; set; }
    public string LVL1_EnovaEmpId { get; set; }
    public string LVL1_EmployeeName { get; set; }
    public string LVL2_EnovaEmpId { get; set; }
    public string LVL2_EmployeeName { get; set; }
    public List<OrganisationRoleForFormVm> Level1Approvers { get; set; }
    public List<OrganisationRoleForFormVm> Level2Approvers { get; set; }
    public List<FormFileVm> FormFiles { get; set; }
    public string NumberPrefix { get; set; }
    public string FolderName { get; set; }
}