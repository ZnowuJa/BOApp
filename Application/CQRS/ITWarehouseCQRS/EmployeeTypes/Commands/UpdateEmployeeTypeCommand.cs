using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Commands;
public class UpdateEmployeeTypeCommand : IRequest<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UpdateEmployeeTypeCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
