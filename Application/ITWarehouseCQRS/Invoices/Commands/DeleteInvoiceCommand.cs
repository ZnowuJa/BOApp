using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Invoices.Commands;
public class DeleteInvoiceCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteInvoiceCommand(int id)
    {
        Id = id;
    }
}
