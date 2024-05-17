using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Assets.Commands;
public class DeleteAssetCommand : IRequest<int>
{
    public int Id { get; set; }
    public DeleteAssetCommand(int id)
    {
        Id = id;
    }
}
