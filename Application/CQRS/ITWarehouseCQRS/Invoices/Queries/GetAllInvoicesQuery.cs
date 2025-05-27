using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Invoices.Queries;
public class GetAllInvoicesQuery : IRequest<IQueryable<InvoiceVm>>
{
}
