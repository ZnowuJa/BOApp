namespace Application.ViewModels.Accounting;

public class CostAllocationVm
{
    public string LongName { get; set; } = string.Empty;
    public int EnovaEmpId { get; set; } = 0;
    public string Location { get; set; } = string.Empty;
    public decimal Allocation { get; set; } = 0;
}