using Application.ViewModels;

using MediatR;

namespace Application.ITWarehouseCQRS.AssetHistories.Queries;
public class GetAllAssetHistoryByAssetIdQuery : IRequest<IQueryable <AssetHistoryVm>>
{
    public int AssetId { get; set; }
    public GetAllAssetHistoryByAssetIdQuery(int i)
    {
        AssetId = i;
    }
}
