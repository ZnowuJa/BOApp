using Domain.Common;

namespace Domain.Forms;
public class OnboardingForm : FormTemplate
{
    public OnboardingForm() : base("Formularz Onboarding", "Formularz do rejestrowania postępów w Onboardingu pracowników", "onboardingForm", "ONB", "CoC", "Rejestracja", 2)
    {
        Statuses = new List<string> 
        {
            "Rejestracja", "W trakcie", "Zakończony" 
        };
    }

    // Properties specific to Onboarding Form
    
    public string Group { get; set; }
    public string? Note { get; set; }
    public int? Progress { get; set; }
    public bool FirstRun { get; set; } = false;
    public int? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public int ManagerId { get; set; }
    public string Instructions { get; set; }
    public DateTime? Requested { get; set; }
    public string Approvals { get; set; }
    public string Level1Approvers { get; set; }
    public string Level2Approvers { get; set; }
    public string LVL1_EnovaEmpId { get; set; }
    public string LVL2_EnovaEmpId { get; set; }
    public string LVL1_EmployeeName { get; set; }
    public string LVL2_EmployeeName { get; set; }
    //public List<FormFile> FormFiles { get; set; }

}