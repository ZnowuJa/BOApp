using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.BusinessPartners.Queries
{
    public class GetAllBusinessPartnerQuery : IRequest<IQueryable<BusinessPartnerVm>>
    {
    }

    public class GetAllBusinessPartnerQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllBusinessPartnerQuery, IQueryable<BusinessPartnerVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IQueryable<BusinessPartnerVm>> Handle(GetAllBusinessPartnerQuery request, CancellationToken cancellationToken)
        {
            var businessPartners = await _appDbContext.BusinessPartners
                                                      .Where(bp => bp.StatusId == 1)
                                                      .AsNoTracking()
                                                      .ToListAsync(cancellationToken);

            var businessPartnerVms = _mapper.Map<List<BusinessPartnerVm>>(businessPartners);
            return businessPartnerVms.AsQueryable();
        }
    }
}