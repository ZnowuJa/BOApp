using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Warehouses.Queries;
public class GetWarehouseQuery : IRequest<WarehouseVm>
{
    public GetWarehouseQuery(int i)
    {
        WarehouseId = i;
    }

    public int WarehouseId { get; set; }
}
