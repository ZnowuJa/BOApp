using Application.ViewModels;
using Application.ViewModels.Settlement;
using MediatR;

namespace Application.CQRS.SettlementCQRS.CostCenters.Queries
{
    public class GetAllCostCenterQuery : IRequest<IQueryable<CostCenterVm>>
    {
    }
}
