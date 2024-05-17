using Application.ViewModels;
using MediatR;

namespace Application.ITWarehouseCQRS.Currencies.Queries;
public class GetCurrencyQuery : IRequest<CurrencyVm>
{
    public GetCurrencyQuery(int i)
    {
        CurrencyId = i;
    }

    public int CurrencyId { get; set; }
}
