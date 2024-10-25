using Application.ViewModels;
using Application.ViewModels.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.Countries.Queries
{
    public class GetAllCountryQuery : IRequest<IQueryable<CountryVm>>
    {
    }
}
