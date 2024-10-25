using Application.ViewModels;
using Application.ViewModels.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.CostCenters.Queries
{
    public class GetAllCostCenterQuery : IRequest<IQueryable<CostCenterVm>>
    {
    }
}
