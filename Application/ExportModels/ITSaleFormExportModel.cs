namespace Application.ExportModels;
public class ITSaleFormExportModel
{
    public int Id { get; set; }
    public string Status { get; set; } 
    public string? Number { get; set; } 
 

    public string? Note { get; set; }
    public int? OperatorId { get; set; }
    public string? OperatorName { get; set; }

    public string LVL1_EnovaEmpId { get; set; } = string.Empty;
    public string LVL2_EnovaEmpId { get; set; } = string.Empty;
    public string LVL1_EmployeeName { get; set; } = string.Empty;
    public string LVL2_EmployeeName { get; set; } = string.Empty;

    public int? CompanyId { get; set; }
    public int? CompanyName { get; set; }
    public int? EmployeeId { get; set; }
    public int? EmployeeName { get; set; }
    public string? AssetIds { get; set; }
}
