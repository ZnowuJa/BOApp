using Application.DTOs;

using MediatR;

namespace Application.ITWarehouseCQRS.Assets.Queries;
public class GetAllAssetsDTObyEmpIdQuery : IRequest<IQueryable<AssetDTO>>
{
    public int uId {  get; set; } 

    public GetAllAssetsDTObyEmpIdQuery(int _uId)
    {
        uId = _uId;

    }
}
