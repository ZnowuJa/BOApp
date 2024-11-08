using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.VATRates.Queries
{
    public class GetVATRateQuery(int i) : IRequest<VATRateVm>
    {
        public int VATRateId { get; set; } = i;
    }

    public class GetVATRateQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetVATRateQuery, VATRateVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<VATRateVm> Handle(GetVATRateQuery request, CancellationToken cancellationToken)
        {
            var vatRate = await _appDbContext.VATRates
                                             .Where(v => v.Id == request.VATRateId)
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(cancellationToken);
            return _mapper.Map<VATRateVm>(vatRate);
        }
    }
}
