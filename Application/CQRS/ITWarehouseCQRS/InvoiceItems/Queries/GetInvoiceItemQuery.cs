using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.InvoiceItems.Queries;
public class GetInvoiceItemQuery : IRequest<InvoiceItemVm>
{
    public int InvoiceItemId { get; set; }
    public GetInvoiceItemQuery(int i)
    {
        InvoiceItemId = i;
    }
    
}
