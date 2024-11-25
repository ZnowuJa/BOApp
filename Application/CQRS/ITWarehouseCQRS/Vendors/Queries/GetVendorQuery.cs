using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Vendors.Queries;
public class GetVendorQuery(int i) : IRequest<VendorVm>
{
    public int VendorId { get; set; } = i;
}
public class GetVendorQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetVendorQuery, VendorVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public IMapper Mapper { get; }

    public async Task<VendorVm> Handle(GetVendorQuery request, CancellationToken cancellationToken)
    {
        var employeetype = await _appDbContext.Vendors.Where(p => p.Id == request.VendorId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var employeetypeVM = _mapper.Map<VendorVm>(employeetype);
        return employeetypeVM;
    }
}