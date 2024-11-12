using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.ITWarehouseCQRS.Vendors.Queries;
public class GetAllVendorsForSelectQuery : IRequest<IQueryable<VendorVm>>
{
}
public class GetAllVendorsForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllVendorsForSelectQueryHandler> logger) : IRequestHandler<GetAllVendorsForSelectQuery, IQueryable<VendorVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger _logger = logger;

    public async Task<IQueryable<VendorVm>> Handle(GetAllVendorsForSelectQuery request, CancellationToken cancellationToken)
    {
        List<Vendor> itemsSelected = new();
        Vendor itemFirst = new Vendor() { Id = 0, Name = "Select..." };
        itemsSelected.Add(itemFirst);
        var itemsFromDb = await _appDbContext.Vendors.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        itemsSelected.AddRange(itemsFromDb);
        var itemsList = _mapper.Map<List<VendorVm>>(itemsSelected);

        return itemsList.AsQueryable();
    }
}