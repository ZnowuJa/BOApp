using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Vendors.Commands;
public class CreateVendorCommand : IRequest<int>
{
    public string Name { get; set; }

    public CreateVendorCommand(string name)
    {
        Name = name;
    }
}
