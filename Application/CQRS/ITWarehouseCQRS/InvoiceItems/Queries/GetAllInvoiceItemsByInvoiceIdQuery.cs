using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Invoices.Queries;
public class GetAllInvoiceItemsByInvoiceIdQuery : IRequest<List<InvoiceItemVm>>
{
    public int InvoiceId { get; set; }
    public GetAllInvoiceItemsByInvoiceIdQuery(int i)
    {
        InvoiceId = i;
    }
}
