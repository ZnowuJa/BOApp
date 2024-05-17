using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Commands;
public class CreateEmployeeTypeCommand : IRequest<int>
{
    public string Name { get; set; }

    public CreateEmployeeTypeCommand(string name)
    {
        Name = name;
    }
}
