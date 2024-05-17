using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.States.Queries;
public class GetStateQuery : IRequest<StateVm>
{
    public GetStateQuery(int i)
    {
        StateId = i;
    }

    public int StateId { get; set; }
}
