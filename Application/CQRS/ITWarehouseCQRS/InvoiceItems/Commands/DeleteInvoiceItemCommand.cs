using MediatR;

namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class DeleteInvoiceItemCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteInvoiceItemCommand(int id)
    {
        Id = id;
    }
}
