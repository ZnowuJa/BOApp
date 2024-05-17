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

namespace Application.ITWarehouseCQRS.AssetsById.Queries;
public class GetAllAssetsByIdQueryHandler : IRequestHandler<GetAllAssetsByIdQuery, IQueryable<AssetVmById>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    public GetAllAssetsByIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<IQueryable<AssetVmById>> Handle(GetAllAssetsByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Assets.Where(p => p.StatusId == 1)
            .Include(i => i.Part)
            .Include(i => i.Invoice)
            .Include(i => i.State)
            .Include(i => i.Warehouse)
            .Include(i => i.Currency)
            .ToListAsync(cancellationToken);
        var res = _mapper.Map<List<AssetVmById>>(result);
        return res.AsQueryable();
    }
}