using Application.ViewModels.General;

namespace Application.ViewModels.Accounting;

public class InvoiceMapping
{
    public int Id { get; set; }
    public SapCostCenterVm SapCostCenter {get; set;}
    public LocationVm? Location { get; set; } = new();
    public CostCenterVm CostCenter { get; set; } = new();
    public GLAccountVm GLAccount { get; set; } = new();
    public VATRateVm VATRate { get; set; } = new();
    public decimal AmountNet { get; set; } = 0m;
}