using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Units.Commands;
public class CreateUnitCommand : IRequest<int>
{
    public string Name { get; set; }
    public string ShortName { get; set; }

    public CreateUnitCommand(string name, string shortName)
    {
        Name = name;
        ShortName = shortName;
    }
}
