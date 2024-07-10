using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Parts.Queries;
public class GetPartQuery : IRequest<PartVm>
{
    public GetPartQuery(int i)
    {
        PartId = i;
    }
    public int PartId { get; set; }
}
