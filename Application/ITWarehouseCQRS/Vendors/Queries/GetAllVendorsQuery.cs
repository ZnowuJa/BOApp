using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Vendors.Queries;
public class GetAllVendorsQuery : IRequest<IQueryable<VendorVm>>
{
}
