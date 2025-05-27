namespace Application.ViewModels.Accounting;

public class Bill
{
    public int Id { get; set; }
    public decimal Amount { get; set; } = 0m;
    public string Currency { get; set; } = "PLN";
    public string Reason { get; set; } = string.Empty;
    public List<BillFile> BillFiles { get; set; } = new();
    public bool Invoice { get; set; } = false;
    public string InvoiceTitle { get; set; } = string.Empty;
    public BusinessPartnerVm BusinessPartner { get; set; } = new();
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public List<InvoiceMapping> InvoiceMappings { get; set; } = new();
    public bool isParking { get; set; } = false;
    public decimal ParkingAmount { get; set; } = 0m;

}

