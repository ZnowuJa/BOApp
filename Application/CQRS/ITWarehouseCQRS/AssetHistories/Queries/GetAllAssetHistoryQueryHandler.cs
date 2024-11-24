using Application.Interfaces;
using Application.ViewModels;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.AssetHistories.Queries;
public class GetAllAssetHistoryByAssetIdQueryHandler : IRequestHandler<GetAllAssetHistoryByAssetIdQuery, IQueryable<AssetHistoryVm>>
{
    private readonly IAppDbContext _appDbContext;
    private IMapper _mapper;
    public GetAllAssetHistoryByAssetIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    public async Task<IQueryable<AssetHistoryVm>> Handle(GetAllAssetHistoryByAssetIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.AssetsHistory.Where(a => a.AssetId == request.AssetId.ToString()).ToListAsync(cancellationToken);
        var res = _mapper.Map<List<AssetHistoryVm>>(result);

        return res.AsQueryable();
    }
}
