using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.Vendors.Queries;
public class GetVendorQueryHandler : IRequestHandler<GetVendorQuery, VendorVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public GetVendorQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IMapper Mapper { get; }

    public async Task<VendorVm> Handle(GetVendorQuery request, CancellationToken cancellationToken)
    {
        var employeetype = await _appDbContext.Vendors.Where(p => p.Id == request.VendorId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var employeetypeVM = _mapper.Map<VendorVm>(employeetype);
        return employeetypeVM;
    }
}
