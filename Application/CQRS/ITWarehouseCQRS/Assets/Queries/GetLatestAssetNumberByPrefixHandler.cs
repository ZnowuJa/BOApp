using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ITWarehouseCQRS.Assets.Queries;
public class GetLatestAssetNumberByPrefixHandler : IRequestHandler<GetLatestAssetNumberByPrefixQuery, string>
{
    private readonly IAppDbContext _appDbContext;

    public GetLatestAssetNumberByPrefixHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<string> Handle(GetLatestAssetNumberByPrefixQuery request, CancellationToken cancellationToken)
    {
        string prefix = request._prefix;
        var assetTagNumbers = await _appDbContext.Assets
            .Where(asset => asset.AssetTagNumber.StartsWith(prefix))
            .Select(asset => asset.AssetTagNumber)
            .ToListAsync();

        int highestAssetTagNumber = assetTagNumbers
            .Select(assetTagNumber => int.Parse(assetTagNumber.Substring(prefix.Length)))
            .OrderByDescending(n => n)
            .FirstOrDefault();


        //string highestAssetTagNumber = await _appDbContext.Assets
        //    .Where(asset => asset.AssetTagNumber.StartsWith(request._prefix))
        //    .Select(asset => asset.AssetTagNumber)
        //    .OrderByDescending(n => n)
        //    .FirstOrDefaultAsync() ?? string.Empty;

        return highestAssetTagNumber.ToString();
    }
}
