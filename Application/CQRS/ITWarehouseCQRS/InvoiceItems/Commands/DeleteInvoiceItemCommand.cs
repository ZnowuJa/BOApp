using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class DeleteInvoiceItemCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteInvoiceItemCommand(int id)
    {
        Id = id;
    }
}
