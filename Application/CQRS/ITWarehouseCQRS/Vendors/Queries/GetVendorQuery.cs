using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Vendors.Queries;
public class GetVendorQuery : IRequest<VendorVm>
{
    public GetVendorQuery(int i)
    {
        VendorId = i;
    }

    public int VendorId { get; set; }
}
