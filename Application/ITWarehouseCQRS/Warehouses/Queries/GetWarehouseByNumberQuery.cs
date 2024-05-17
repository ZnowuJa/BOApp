using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Warehouses.Queries;
public class GetWarehouseByNumberQuery : IRequest<WarehouseVm>
{
    public GetWarehouseByNumberQuery(string n)
    {
        WarehouseNumber = n;
    }

    public string WarehouseNumber { get; set; }
}
