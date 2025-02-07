using Application.ViewModels.General;

using AutoMapper;
using Domain.WorkFlows;

namespace Application.Forms;

public class OtherFormVm 
{
    // Properties from FormTemplate
    public int Id { get; set; }
    public string Name { get; set; } = "Płatność odroczona";
    public string Description { get; set; } = "Formularz do ustawiania płatności odroczonych w Autostacji.";
    public string FolderName { get; set; } = "deferralPayment";
    public string NumberPrefix { get; set; } = "DP";
    public string Status { get; set; }
    public List<string> Statuses { get; set; }
    public int WorkflowTemplateId { get; set; }
    public WorkflowTemplate WorkflowTemplate { get; set; }

    // Properties specific to DeferralPaymentForm
    public string? Number { get; set; }
    public string? KontrahentId { get; set; }
    public string? KontrahentName { get; set; }
    public bool? KontrahentPrzelew { get; set; }
    public bool? Przelew { get; set; }
    public string? Note { get; set; }
    public int? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public DateTime? Requested { get; set; }
    public List<ApprovalVm>? Approvals { get; set;}
    public List<OrganisationRoleForFormVm> Level1Approvers { get; set;}
    public List<OrganisationRoleForFormVm> Level2Approvers { get; set;}

    public string LVL1_EnovaEmpId {  get; set; }
    public string LVL2_EnovaEmpId { get; set; }
    public string LVL1_EmployeeName { get; set; }
    public string LVL2_EmployeeName { get; set; }




    public void Mapping(Profile profile)
    {
       
    }

    public OtherFormVm()
    {
        Status = "Rejestracja";
        Statuses = GetDefaultStatuses();

    }

    public static List<string> GetDefaultStatuses()
    {
        return new List<string>
            {
                "Rejestracja", "Level1", "Level2", "DeadEnd"
            };
    }
}
