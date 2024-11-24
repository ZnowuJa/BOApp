namespace Application.ViewModels.General;
public class Approval
{
    public string Status { get; set; }
    public string EnovaEmpId { get; set; }
    public string LongName { get; set; }
    public DateTime Date { get; set; }
    public bool IsApproved { get; set; } = false;
    
}
