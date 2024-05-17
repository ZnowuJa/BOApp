using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Currencies.Commands;
public class DeleteCurrencyCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteCurrencyCommand(int id)
    {
        Id = id;
    }
}
