using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.ITWarehouseCQRS.Currencies.Queries;
public class GetAllCurrenciesQuery : IRequest<IQueryable<CurrencyVm>>
{
}
