using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Warehouses.Queries;
public class GetWarehouseByNumberQuery : IRequest<WarehouseVm>
{
    public GetWarehouseByNumberQuery(string n)
    {
        WarehouseNumber = n;
    }

    public string WarehouseNumber { get; set; }
}
