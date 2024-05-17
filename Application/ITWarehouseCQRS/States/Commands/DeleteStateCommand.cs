using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.States.Commands;
public class DeleteStateCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteStateCommand(int id)
    {
        Id = id;
    }
}
