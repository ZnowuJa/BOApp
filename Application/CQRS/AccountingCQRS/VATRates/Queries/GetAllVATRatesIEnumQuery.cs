using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.VATRates.Queries
{
    public class GetAllVatRateIEnumQuery : IRequest<IEnumerable<VATRateVm>>
    {
    }
    public class GetAllVatRateIEnumQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllVatRateIEnumQuery, IEnumerable<VATRateVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<VATRateVm>> Handle(GetAllVatRateIEnumQuery request, CancellationToken cancellationToken)
        {
            var vatRates = await _appDbContext.VATRates
                .Where(ct => ct.StatusId == 1)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<VATRateVm>>(vatRates);
        }
    }
}