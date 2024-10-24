using Application.Interfaces;
using Application.ITWarehouseCQRS.States.Queries;
using Application.ViewModels;
using Application.ViewModels.Settlement;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.SettlementCQRS.VATRates.Queries
{
    public class GetVATRateQueryHandler : IRequestHandler<GetVATRateQuery, VATRateVm>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetVATRateQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

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
