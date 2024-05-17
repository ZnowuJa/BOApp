using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.States.Commands;
public class CreateStateCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Description { get; set; }

    public CreateStateCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
