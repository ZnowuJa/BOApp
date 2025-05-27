using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Employees.Commands;
public class DeleteEmployeeCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteEmployeeCommand(int id)
    {
        Id = id;
    }
}
