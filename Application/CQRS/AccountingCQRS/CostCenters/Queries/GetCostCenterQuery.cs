using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.CostCenters.Queries
{
    public class GetCostCenterQuery(int i) : IRequest<CostCenterVm>
    {
        public int CostCenterId { get; set; } = i;
    }
    public class GetCostCenterQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetCostCenterQuery, CostCenterVm>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

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
