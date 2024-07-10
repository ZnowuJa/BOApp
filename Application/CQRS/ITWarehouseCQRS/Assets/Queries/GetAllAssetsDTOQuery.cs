using Application.DTOs;
using MediatR;

namespace Application.ITWarehouseCQRS.Assets.Queries;
public class GetAllAssetsDTOQuery : IRequest<IQueryable<AssetDTO>>
{

}
