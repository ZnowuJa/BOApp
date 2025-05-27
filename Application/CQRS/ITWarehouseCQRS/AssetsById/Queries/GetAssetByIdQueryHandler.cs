using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.AssetsById.Queries;
public class GetAssetByIdQueryHandler : IRequestHandler<GetAssetByIdQuery, AssetVmById>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mappper;
    public GetAssetByIdQueryHandler(IAppDbContext appDbContext, IMapper mappper)
    {
        _appDbContext = appDbContext;
        _mappper = mappper;
    }
    public async Task<AssetVmById> Handle(GetAssetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Assets.Where(p => p.Id == request.AssetId)
            .Include(a => a.Part).Include(b => b.Invoice).Include(c => c.State).Include(e => e.Warehouse).Include(f => f.Currency).FirstOrDefaultAsync();

        var res = _mappper.Map<AssetVmById>(result);

        return res;
    }
}