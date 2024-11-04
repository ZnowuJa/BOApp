using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.VATRates.Queries
{
    public class GetAllVATRateQuery : IRequest<IQueryable<VATRateVm>>
    {
    }
    public class GetAllVATRateQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllVATRateQuery, IQueryable<VATRateVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<VATRateVm>> Handle(GetAllVATRateQuery request, CancellationToken cancellationToken)
        {
            var vatRates = await _appDbContext.VATRates
                                               .Where(ct => ct.StatusId == 1)
                                               .AsNoTracking()
                                               .ToListAsync(cancellationToken);

            var vatRateVms = _mapper.Map<List<VATRateVm>>(vatRates);
            return vatRateVms.AsQueryable();
        }
    }
}
