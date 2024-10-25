using Application.ViewModels;
using Application.ViewModels.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.VATRates.Queries
{
    public class GetAllVATRateQuery : IRequest<IQueryable<VATRateVm>>
    {
    }
}
