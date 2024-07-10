using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Assets.Queries;
public class GetAssetQueryHandler : IRequestHandler<GetAssetQuery, AssetVm>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mappper;
    public GetAssetQueryHandler(IAppDbContext appDbContext, IMapper mappper)
    {
        _appDbContext = appDbContext;
        _mappper = mappper;
    }
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