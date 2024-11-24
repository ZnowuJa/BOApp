using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Assets.Queries;
public class GetAssetQuery : IRequest<AssetVm>
{
    public GetAssetQuery(int i)
    {
        AssetId = i;
    }
    public int AssetId { get; set; }
}
