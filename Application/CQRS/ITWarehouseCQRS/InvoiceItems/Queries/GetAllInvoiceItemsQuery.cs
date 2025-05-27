using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Invoices.Queries;
public class GetAllInvoiceItemsQuery : IRequest<IQueryable<InvoiceItemVm>>
{
}
