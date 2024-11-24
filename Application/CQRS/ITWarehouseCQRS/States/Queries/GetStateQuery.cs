using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.States.Queries;
public class GetStateQuery : IRequest<StateVm>
{
    public GetStateQuery(int i)
    {
        StateId = i;
    }

    public int StateId { get; set; }
}
