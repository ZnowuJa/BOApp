using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.ITWarehouseCQRS.Assets.Queries;
public class GetLatestAssetNumberByPrefixQuery(string prefix) : IRequest<string>
{
    public string _prefix { get; set; } = prefix;
}
public class GetLatestAssetNumberByPrefixHandler(IAppDbContext appDbContext) : IRequestHandler<GetLatestAssetNumberByPrefixQuery, string>
{
    private readonly IAppDbContext _appDbContext = appDbContext;

    public async Task<string> Handle(GetLatestAssetNumberByPrefixQuery request, CancellationToken cancellationToken)
    {
        string prefix = request._prefix;
        prefix = prefix + '0';
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