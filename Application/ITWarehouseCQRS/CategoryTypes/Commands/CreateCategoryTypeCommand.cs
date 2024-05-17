using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CategoryTypes.Commands;
public class CreateCategoryTypeCommand : IRequest<int>
{
    public string Name { get; set; }

    public CreateCategoryTypeCommand(string name)
    {
        Name = name;
    }
}
