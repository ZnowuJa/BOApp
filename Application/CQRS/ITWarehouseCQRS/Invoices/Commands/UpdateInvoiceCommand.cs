using MediatR;

namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class UpdateInvoiceCommand : IRequest<int>
{
    public int Id { get; set; }
    public string? Number { get; set; }
    public int CompanyVmId { get; set; }
    public DateTime? Date { get; set; }
    public decimal TotalNet { get; set; }
    public int CurrencyVmId { get; set; }

    public UpdateInvoiceCommand(int id, string number, int companyVmId, DateTime? date, decimal totalNet, int currencyVmId)
    {
        Id = id;
        Number = number;
        CompanyVmId = companyVmId;
        Date = date;
        TotalNet = totalNet;
        CurrencyVmId = currencyVmId;

    }
}
