using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Invoices.Queries;
public class GetInvoiceQuery : IRequest<InvoiceVm>
{
    public GetInvoiceQuery(int i)
    {
        InvoiceId = i;
    }
    public int InvoiceId { get; set; }
}
