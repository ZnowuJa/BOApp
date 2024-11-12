using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.ITWarehouseCQRS.Vendors.Queries;
public class GetAllVendorsQuery : IRequest<IQueryable<VendorVm>>
{
}
public class GetAllVendorsQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllVendorsQueryHandler> logger) : IRequestHandler<GetAllVendorsQuery, IQueryable<VendorVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<VendorVm>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
    {
        var curs = await _appDbContext.Vendors.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var curslist = _mapper.Map<List<VendorVm>>(curs);

        return curslist.AsQueryable();
    }
}