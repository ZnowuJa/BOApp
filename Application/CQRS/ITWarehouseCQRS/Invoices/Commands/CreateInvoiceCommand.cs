using MediatR;

namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class CreateInvoiceCommand : IRequest<int>
{

    public int Id { get; set; }
    public string? Number { get; set; }
    public int CompanyVmId { get; set; }
    public DateTime? Date { get; set; }
    public decimal TotalNet { get; set; }
    public int CurrencyVmId { get; set; }


    public CreateInvoiceCommand(string number, int companyVmId, DateTime? date, decimal totalNet, int currencyVmId)
    {
        Number = number;
        CompanyVmId = companyVmId;  
        Date = date;
        TotalNet = totalNet;
        CurrencyVmId = currencyVmId;

    }
}
