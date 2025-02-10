namespace Application.ViewModels.Accounting;

public class Bill
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "PLN";
    public string Reason { get; set; }
    public List<BillFile> BillFiles { get; set; } = new();
    public bool Invoice { get; set; } = false;
    public string InvoiceTitle { get; set; } = string.Empty;
    public BusinessPartner BusinessPartner { get; set; } = new();
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public List<InvoiceMapping> InvoiceMappings { get; set; } = new();

}

