using MediatR;
using Application.ViewModels;

namespace Application.ITWarehouseCQRS.Currencies.Queries;
public class GetAllCurrenciesQuery : IRequest<IQueryable<CurrencyVm>>
{
}
