using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Invoices.Queries;
public class GetAllInvoiceItemsForSelectQuery : IRequest<IQueryable<InvoiceItemVm>>
{
}
