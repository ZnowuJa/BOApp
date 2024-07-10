using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Vendors.Queries;
public class GetVendorQuery : IRequest<VendorVm>
{
    public GetVendorQuery(int i)
    {
        VendorId = i;
    }

    public int VendorId { get; set; }
}
