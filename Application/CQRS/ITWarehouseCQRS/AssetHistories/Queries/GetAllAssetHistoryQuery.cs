using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels;

using MediatR;

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.ITWarehouseCQRS.AssetHistories.Queries;
public class GetAllAssetHistoryByAssetIdQuery : IRequest<IQueryable <AssetHistoryVm>>
{
    public int AssetId { get; set; }
    public GetAllAssetHistoryByAssetIdQuery(int i)
    {
        AssetId = i;
    }
}
