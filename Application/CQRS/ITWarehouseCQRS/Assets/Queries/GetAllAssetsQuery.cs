using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Assets.Queries;
public class GetAllAssetsQuery : IRequest<IQueryable<AssetVm>>
{
}
