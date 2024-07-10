using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.CategoryTypes.Commands;
public class DeleteCategoryTypeCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteCategoryTypeCommand(int id)
    {
        Id = id;
    }
}
