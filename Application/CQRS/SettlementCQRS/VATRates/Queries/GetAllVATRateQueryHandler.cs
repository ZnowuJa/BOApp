using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels.Settlement;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.SettlementCQRS.VATRates.Queries
{
    public class GetAllVATRateQueryHandler : IRequestHandler<GetAllVATRateQuery, IQueryable<VATRateVm>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetAllVATRateQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

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
