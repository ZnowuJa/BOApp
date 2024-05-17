using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Notes.Commands;
public class DeleteNoteCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteNoteCommand(int id)
    {
        Id = id;
    }
}
