using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.ViewModels.Accounting;
using MediatR;

namespace Application.CQRS.AccountingCQRS.Dictionaries
{
    public class GetNbpCurrencyRatesByDateQuery: IRequest<IQueryable<NbpCurrencyRateVm>>
    {
        public DateOnly RateDate { get; }

        // You can add additional properties for date range if needed
        public GetNbpCurrencyRatesByDateQuery(DateOnly rateDate)
        {
            RateDate = rateDate;
        }
    }
    public class GetNbpCurrencyRatesByDateQueryHandler : IRequestHandler<GetNbpCurrencyRatesByDateQuery, IQueryable<NbpCurrencyRateVm>>
    {
        private readonly IAppDbContext _context;

        public GetNbpCurrencyRatesByDateQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<NbpCurrencyRateVm>> Handle(GetNbpCurrencyRatesByDateQuery request, CancellationToken cancellationToken)
        {
            // Filter the records based on RateDate
            var query = _context.NbpCurrencyRates
                .Where(c => c.RateDate <= request.RateDate && c.RateDate > request.RateDate.AddDays(-60)) // Filter by RateDate
                .OrderByDescending(c => c.RateDate)
                .Select(c => new NbpCurrencyRateVm(
                    c.Id,
                    c.Currency,
                    c.Code,
                    c.Mid,
                    c.RateDate
                ))
                .AsQueryable();

            return query;
        }
    }
}

