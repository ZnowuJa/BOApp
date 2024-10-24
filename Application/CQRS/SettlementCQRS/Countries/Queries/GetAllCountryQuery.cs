using Application.ViewModels;
using Application.ViewModels.Settlement;
using MediatR;

namespace Application.CQRS.SettlementCQRS.Countries.Queries
{
    public class GetAllCountryQuery : IRequest<IQueryable<CountryVm>>
    {
    }
}
