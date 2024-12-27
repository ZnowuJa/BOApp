using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.Countries.Queries
{
    public class GetAllCountryQuery : IRequest<IQueryable<CountryVm>>
    {
    }
    public class GetAllCountryQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllCountryQuery, IQueryable<CountryVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<CountryVm>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {
            var countries = await _appDbContext.Countries
                                               .Where(ct => ct.StatusId == 1)
                                               .AsNoTracking()
                                               .ToListAsync(cancellationToken);
            // var countryVms = _mapper.Map<List<CountryVm>>(countries);
            var currencies = await _appDbContext.Currencies
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);
            var countryVms = countries.Select(country =>
            {
                var currency = currencies.FirstOrDefault(c => c.Id == country.CurrencyId);
                return new CountryVm
                {
                    Id = country.Id,
                    CountryCode = country.CountryCode,
                    Name = country.Name,
                    IsEU = country.IsEU,
                    IsPL = country.IsPL,
                    CurrencyId = country.CurrencyId,
                    CurrencyVmId = currency?.Id,
                    CurrencyVmName = currency?.Name,
                    Allowance = country.Allowance,
                    AllowanceFirstDay8H = country.AllowanceFirstDay8H,
                    AllowanceFirstDay12H = country.AllowanceFirstDay12H,
                    AllowanceNextDay8H = country.AllowanceNextDay8H,
                    AllowanceNextDay12H = country.AllowanceNextDay12H,
                    BreakfastReduction = country.BreakfastReduction,
                    LunchReduction = country.LunchReduction,
                    DinnerReduction = country.DinnerReduction,
                    AccomodationAllowance = country.AccomodationAllowance,
                    TravelAllowance = country.TravelAllowance,
                    LocalTravelAllowance = country.LocalTravelAllowance
                };
            }).ToList();
            return countryVms.AsQueryable();
        }
    }
}
