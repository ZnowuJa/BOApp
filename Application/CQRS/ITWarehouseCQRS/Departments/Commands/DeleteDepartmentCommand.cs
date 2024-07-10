using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Departments.Commands;
public class DeleteDepartmentCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteDepartmentCommand(int id)
    {
        Id = id;
    }
}
