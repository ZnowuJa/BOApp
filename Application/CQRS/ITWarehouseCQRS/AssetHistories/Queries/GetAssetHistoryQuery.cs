using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels;

using MediatR;

namespace Application.ITWarehouseCQRS.AssetHistories.Queries;
public class GetAssetHistoryQuery : IRequest<AssetHistoryVm>
{
    public GetAssetHistoryQuery(int i)
    {
        AssetId = i;
    }
    public int AssetId { get; set; }
}
