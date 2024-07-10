using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Warehouses.Queries;
public class GetWarehouseQuery : IRequest<WarehouseVm>
{
    public GetWarehouseQuery(int i)
    {
        WarehouseId = i;
    }

    public int WarehouseId { get; set; }
}
