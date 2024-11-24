using Application.ViewModels;

using MediatR;

namespace Application.ITWarehouseCQRS.Units.Queries;
public class GetUnitQuery : IRequest<UnitVm>
{
    public GetUnitQuery(int i)
    {
        UnitId = i;
    }

    public int UnitId { get; set; }
}
