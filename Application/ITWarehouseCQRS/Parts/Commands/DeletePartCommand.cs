using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Parts.Commands;
public class DeletePartCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeletePartCommand(int id)
    {
        Id = id;
    }
}
