using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.AssetsById.Queries;
public class GetAllAssetsByIdForSelectQuery : IRequest<IQueryable<AssetVmById>>
{
}
