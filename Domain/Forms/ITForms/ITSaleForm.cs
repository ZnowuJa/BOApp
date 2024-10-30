using Domain.Common;
using Domain.Entities.Common;
using Domain.Entities.ITWarehouse;

namespace Domain.Forms.ITForms;
public class ITSaleForm : FormTemplate
{
    public ITSaleForm() : base("Sprzedaż sprzętu IT", "Formularz do rejestrowania sprzedaży sprzętu IT", "ITSaleForm", "ITSALE", "IT", "Rejestracja", 2)
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


    public List<FormFile> FormFiles { get; set; }
    public int CompanyId { get; set; }
    public Company? Company { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public ICollection<Asset>? Assets { get; set; }
}
