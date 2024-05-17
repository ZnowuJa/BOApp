using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Assets.Queries;
public class GetAllAssetsForSelectQuery : IRequest<IQueryable<AssetVm>>
{
}
