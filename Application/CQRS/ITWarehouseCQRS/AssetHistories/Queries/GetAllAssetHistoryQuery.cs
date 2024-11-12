using Application.ViewModels;
using MediatR;
using Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.AssetHistories.Queries;
public class GetAllAssetHistoryByAssetIdQuery(int i) : IRequest<IQueryable<AssetHistoryVm>>
{
    public int AssetId { get; set; } = i;
}
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