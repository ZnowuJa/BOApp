using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities.ITWarehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Assets.Queries;
public class GetAllAssetsForSelectQuery : IRequest<IQueryable<AssetVm>>
{
}
internal class GetAllAssetsForSelectQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllAssetsForSelectQuery, IQueryable<AssetVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private IMapper _mapper = mapper;

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