using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Units.Commands;
public class DeleteUnitCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteUnitCommand(int id)
    {
        Id = id;
    }
}
