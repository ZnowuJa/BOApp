using Domain.Common;

namespace Domain.Forms.ITForms;
public class ITScrappingForm : FormTemplate
{
    public ITScrappingForm() : base("Formularz Złomowania sprzętu IT", "Formularz do rejestrowania złomowania sprzętu It", "ITscrappingForm", "ITSCRAP", "IT", "Rejestracja", 2)
    {
        Statuses = new List<string>
        {
            "Rejestracja", "W trakcie", "Zakończony"
        };
    }

    public string? Note { get; set; }
    public int? OperatorId { get; set; }
    public string? OperatorName { get; set; }
 

    public string Approvals { get; set; }
    public string Level1Approvers { get; set; }
    public string Level2Approvers { get; set; }
    public string LVL1_EnovaEmpId { get; set; }
    public string LVL2_EnovaEmpId { get; set; }
    public string LVL1_EmployeeName { get; set; }
    public string LVL2_EmployeeName { get; set; }

    public int CompanyId { get; set; }

    public List<int>? Assets { get; set; }
}
