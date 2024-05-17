using Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ITWarehouseCQRS.AssetsById.Queries;
public class GetAllAssetsByIdQuery : IRequest<IQueryable<AssetVmById>>
{
}
