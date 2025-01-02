namespace Application.ViewModels.Accounting;

public class Bill
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Reason { get; set; }
    public string? FilePath { get; set; }
    public string? AttUrl { get; set; }
    public string? OriginalFileName { get; set; }
    public List<InvoiceMapping> Posting { get; set; }
    
}