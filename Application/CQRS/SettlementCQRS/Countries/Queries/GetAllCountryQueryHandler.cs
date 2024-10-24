using Application.Interfaces;
using Application.ViewModels.Settlement;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.SettlementCQRS.Countries.Queries
{
    public class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQuery, IQueryable<CountryVm>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetAllCountryQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

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
