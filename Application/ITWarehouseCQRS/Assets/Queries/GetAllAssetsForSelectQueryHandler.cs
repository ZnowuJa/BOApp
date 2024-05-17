using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Assets.Queries;
internal class GetAllAssetsForSelectQueryHandler : IRequestHandler<GetAllAssetsForSelectQuery, IQueryable<AssetVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;

    public GetAllAssetsForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;

    }

    public async Task<IQueryable<AssetVm>> Handle(GetAllAssetsForSelectQuery request, CancellationToken cancellationToken)
    {
        Asset item = new Asset() { Id = 0, AssetTagNumber = "Select..." };
        List<Asset> itemList = [item];
        var result = await _appDbContext.Assets.Where(p => p.StatusId == 1).ToListAsync(cancellationToken);
        itemList.AddRange(result);
        var res = _mapper.Map<List<AssetVm>>(itemList);
        return res.AsQueryable();
    }
}

