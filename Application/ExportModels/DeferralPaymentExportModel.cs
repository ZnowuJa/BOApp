namespace Application.ExportModels;


public class DeferralPaymentExportModel 
{

    public int Id { get; set; }
    public string Status { get; set; }
    // Properties specific to DeferralPaymentForm
    public string? Number { get; set; }
    public string? KontrahentId { get; set; }
    public string? KontrahentName { get; set; }
    public string? Note { get; set; }
    public int? EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public DateTime? Requested { get; set; }
    public string LVL1_EnovaEmpId { get; set; }
    public string LVL2_EnovaEmpId { get; set; }
    public string LVL1_EmployeeName { get; set; }
    public string LVL2_EmployeeName { get; set; }
    public bool isApproved { get; set; }
    public bool isApplied { get; set; }
    public string Numer_Fk { get; set; }
    public bool is_Firma { get; set; }
    public long Faktdoc_Id { get; set; }
    public int CC { get; set; }
    public string VATID { get; set; }
    public string RejectReason { get; set; } = string.Empty;

}
