using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.ViewModels.Accounting;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.Dictionaries
{
    public class GetNbpCurrencyRateByDateAndCodeQuery : IRequest<NbpCurrencyRateVm>
    {
        public DateOnly RateDate { get; }
        public string Code { get; }

        public GetNbpCurrencyRateByDateAndCodeQuery(DateOnly rateDate, string code)
        {
            RateDate = rateDate;
            Code = code;
        }
    }

    public class GetNbpCurrencyRateByDateAndCodeQueryHandler : IRequestHandler<GetNbpCurrencyRateByDateAndCodeQuery, NbpCurrencyRateVm>
    {
        private readonly IAppDbContext _context;

        public GetNbpCurrencyRateByDateAndCodeQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<NbpCurrencyRateVm> Handle(GetNbpCurrencyRateByDateAndCodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.NbpCurrencyRates
                .Where(c => c.Code == request.Code && c.RateDate <= request.RateDate) // Match Code and ensure RateDate is <= request.RateDate
                .OrderByDescending(c => c.RateDate) // Get the most recent RateDate
                .Select(c => new NbpCurrencyRateVm(
                    c.Id,
                    c.Currency,
                    c.Code,
                    c.Mid,
                    c.RateDate
                ))
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}

