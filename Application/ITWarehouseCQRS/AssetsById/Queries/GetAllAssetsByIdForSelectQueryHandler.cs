using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.AssetsById.Queries;
internal class GetAllAssetsByIdForSelectQueryHandler : IRequestHandler<GetAllAssetsByIdForSelectQuery, IQueryable<AssetVmById>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;

    public GetAllAssetsByIdForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }

    public async Task<IQueryable<AssetVmById>> Handle(GetAllAssetsByIdForSelectQuery request, CancellationToken cancellationToken)
    {
        Asset item = new Asset() { Id = 0, AssetTagNumber = "Select..." };
        List<Asset> itemList = [item];
        var result = await _appDbContext.Assets.Where(p => p.StatusId == 1).Include(a => a.Part).Include(b => b.Invoice).Include(c => c.State).Include(e => e.Warehouse).Include(f => f.Currency).ToListAsync(cancellationToken);
        itemList.AddRange(result);
        var res = _mapper.Map<List<AssetVmById>>(itemList);

        return res.AsQueryable();
    }
}

