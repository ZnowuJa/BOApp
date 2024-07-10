using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Invoices.Queries;
public class GetAllInvoicesForSelectQuery : IRequest<IQueryable<InvoiceVm>>
{
}
