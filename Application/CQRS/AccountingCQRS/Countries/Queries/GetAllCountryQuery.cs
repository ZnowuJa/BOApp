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

            var countryVms = _mapper.Map<List<CountryVm>>(countries);
            return countryVms.AsQueryable();
        }
    }
}
