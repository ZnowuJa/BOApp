using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Currencies.Commands;
public class CreateCurrencyCommand : IRequest<int>
{
    public string Name { get; set; }

    public CreateCurrencyCommand(string name)
    {
        Name = name;
    }
}