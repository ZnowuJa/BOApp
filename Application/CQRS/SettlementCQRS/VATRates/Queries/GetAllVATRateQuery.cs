using Application.ViewModels;
using Application.ViewModels.Settlement;
using MediatR;

namespace Application.CQRS.SettlementCQRS.VATRates.Queries
{
    public class GetAllVATRateQuery : IRequest<IQueryable<VATRateVm>>
    {
    }
}
