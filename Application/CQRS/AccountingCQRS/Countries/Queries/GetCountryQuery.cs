using Application.Interfaces;
using Application.ViewModels;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.Countries.Queries
{
    public class GetCountryQuery(int i) : IRequest<CountryVm>
    {
        public int CountryId { get; set; } = i;
    }
    public class GetCountryQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetCountryQuery, CountryVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<CountryVm> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            //var currencies = await _appDbContext.Currencies.ToListAsync(cancellationToken);
            var country = await _appDbContext.Countries
                                             .Where(c => c.Id == request.CountryId)
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(cancellationToken);
            var countryVm = _mapper.Map<CountryVm>(country);
            //var currency = currencies.FirstOrDefault(p => p.Id == countryVm.CurrencyId);
            //countryVm.CurrencyVmName = currency?.Title ?? string.Empty;

            return countryVm;
        }
    }
}
