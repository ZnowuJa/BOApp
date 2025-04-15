using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.BusinessPartners.Queries
{
    public class GetBusinessPartnerQuery(int i) : IRequest<BusinessPartnerVm>
    {
        public int BusinessPartnerId { get; set; } = i;
    }

    public class GetBusinessPartnerQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetBusinessPartnerQuery, BusinessPartnerVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<BusinessPartnerVm> Handle(GetBusinessPartnerQuery request, CancellationToken cancellationToken)
        {
            var businessPartner = await _appDbContext.BusinessPartners
                                                     .Where(b => b.Id == request.BusinessPartnerId)
                                                     .AsNoTracking()
                                                     .FirstOrDefaultAsync(cancellationToken);
            return _mapper.Map<BusinessPartnerVm>(businessPartner);
        }
    }
}