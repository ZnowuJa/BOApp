using MediatR;

namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class DeleteInvoiceCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteInvoiceCommand(int id)
    {
        Id = id;
    }
}
