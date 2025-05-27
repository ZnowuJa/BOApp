using MediatR;

namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class CreateInvoiceItemCommand : IRequest<int>
{

    public string Name { get; set; }
    public int PartId { get; set; }
    public decimal Qty { get; set; }
    public int UnitId { get; set; }
    public decimal UnitNetPrice { get; set; }
    public int CurrencyId { get; set; }
    public int InvoiceId { get; set; }
    public bool ItemsGenerated { get; set; }
    public bool Leasing { get; set; }
    public DateTime? EndOfContract { get; set; }


    public CreateInvoiceItemCommand(string name, int partId, decimal qty, int unitId, decimal unitNetPrice, int currencyId, int invoiceId, bool itemsGenerated, bool leasing, DateTime? endOfContract)
    {
        Name = name;
        PartId = partId;
        Qty = qty;
        UnitId = unitId;
        UnitNetPrice = unitNetPrice;
        CurrencyId = currencyId;
        InvoiceId = invoiceId;
        ItemsGenerated = itemsGenerated;
        Leasing = leasing;
        EndOfContract = endOfContract;

    }
}
