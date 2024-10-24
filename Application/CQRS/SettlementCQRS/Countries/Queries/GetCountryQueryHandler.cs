using Application.Interfaces;
using Application.ITWarehouseCQRS.States.Queries;
using Application.ViewModels;
using Application.ViewModels.Settlement;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.SettlementCQRS.Countries.Queries
{
    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, CountryVm>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetCountryQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<CountryVm> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            var country = await _appDbContext.Countries
                                             .Where(c => c.Id == request.CountryId)
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(cancellationToken);
            return _mapper.Map<CountryVm>(country);
        }
    }
}
