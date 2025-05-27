using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.ViewModels;

using MediatR;

namespace Application.CQRS.ITWarehouseCQRS.AssetHistories.Queries;
public class GetAssetHistoryQuery(int i) : IRequest<AssetHistoryVm>
{
    public int AssetId { get; set; } = i;
}
public class GetAssetHistoryQueryHandler
{
}
