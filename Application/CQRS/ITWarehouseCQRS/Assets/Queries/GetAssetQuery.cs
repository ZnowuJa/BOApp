using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.ITWarehouseCQRS.Assets.Queries;
public class GetAssetQuery(int i) : IRequest<AssetVm>
{
    public int AssetId { get; set; } = i;
}
public class GetAssetQueryHandler(IAppDbContext appDbContext, IMapper mappper) : IRequestHandler<GetAssetQuery, AssetVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mappper = mappper;

    public async Task<AssetVm> Handle(GetAssetQuery request, CancellationToken cancellationToken)
    {
        //Console.WriteLine($"From CQRS: {request.AssetId}");
        var result = await _appDbContext.Assets
            .Where(p => p.Id == request.AssetId)
            .FirstOrDefaultAsync();

        var res = _mappper.Map<AssetVm>(result);
        //Console.WriteLine($"From CQRS: {res.Id}");

        return res;
    }
}