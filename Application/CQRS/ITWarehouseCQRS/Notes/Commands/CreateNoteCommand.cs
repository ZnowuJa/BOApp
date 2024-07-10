using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Notes.Commands;
public class CreateNoteCommand : IRequest<int>
{
    public string Text { get; set; }

    public CreateNoteCommand(string text)
    {
        Text = text;
    }
}