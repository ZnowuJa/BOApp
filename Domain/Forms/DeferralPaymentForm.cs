using Domain.Common;

namespace Domain.Forms;
public class DeferralPaymentForm : FormTemplate
{
    public DeferralPaymentForm() : base("Płatności Odroczone", "Formularz do ustawiania płatności odroczonych w Autostacji.", "deferralPayment", "DP", "Settlements", "Rejestracja", 1)
    {
        Statuses = new List<string> 
        {
            "Rejestracja", "Przełożony", "Rozrachunki", "Zakończone" };
    }

    // Properties specific to DeferralPaymentForm
    
    public string? KontrahentId { get; set; }
    public string? KontrahentName { get; set; }
    public bool KontrahentPrzelew { get; set; } = false;
    public bool Przelew { get; set; } = false;
    public string? Note { get; set; }
    public int? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public DateTime? Requested { get; set; }
    public string Approvals { get; set; }
    public string Level1Approvers { get; set; }
    public string Level2Approvers { get; set; }
    public string LVL1_EnovaEmpId { get; set; }
    public string LVL2_EnovaEmpId { get; set; }
    public string LVL1_EmployeeName { get; set; }
    public string LVL2_EmployeeName { get; set; }
    public bool isApproved { get; set; }
    public bool isApplied {  get; set; }
    public string Numer_Fk { get; set; }
    public bool is_Firma { get; set; }
    public long Faktdoc_Id { get; set; }
    public int CC { get; set; }
    public string VATID { get; set; }
    public string RejectReason { get; set; } = string.Empty;

}