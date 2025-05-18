namespace Application.ViewModels.General;
public class ApprovalVm
{

    public string Status { get; set; } = string.Empty;
    public string EnovaEmpId { get; set; } = string.Empty;
    public string LongName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Now;
    public bool IsApproved { get; set; } = false;
    
}
