using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.EmployeeTypes.Commands;
public class DeleteEmployeeTypeCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteEmployeeTypeCommand(int id)
    {
        Id = id;
    }
}
