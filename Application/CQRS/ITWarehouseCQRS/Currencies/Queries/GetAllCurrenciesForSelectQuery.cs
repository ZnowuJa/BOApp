using MediatR;
using Application.ViewModels;

namespace Application.ITWarehouseCQRS.Currencies.Queries;
public class GetAllCurrenciesForSelectQuery : IRequest<IQueryable<CurrencyVm>>
{
}
