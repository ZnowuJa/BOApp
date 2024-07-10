using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CategoryTypes.Commands;
public class UpdateCategoryTypeCommand : IRequest<int>
{
    public int Id { get; set; } 
    public string Name { get; set;}
    public UpdateCategoryTypeCommand(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
