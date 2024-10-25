using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.CostCenters.Queries
{
    public class GetCostCenterQueryHandler : IRequestHandler<GetCostCenterQuery, CostCenterVm>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetCostCenterQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public IMapper Mapper { get; }
        public async Task<CostCenterVm> Handle(GetCostCenterQuery request, CancellationToken cancellationToken)
        {
            var costCenter = await _appDbContext.CostCenters
                                            .Where(p => p.Id == request.CostCenterId)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(cancellationToken);
            return _mapper.Map<CostCenterVm>(costCenter);
        }
    }
}
