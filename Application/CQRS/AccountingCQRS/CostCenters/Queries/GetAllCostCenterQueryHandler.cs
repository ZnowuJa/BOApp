using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.AccountingCQRS.CostCenters.Queries
{
    public class GetAllCostCenterQueryHandler : IRequestHandler<GetAllCostCenterQuery, IQueryable<CostCenterVm>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetAllCostCenterQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IQueryable<CostCenterVm>> Handle(GetAllCostCenterQuery request, CancellationToken cancellationToken)
        {
            var costCenters = await _appDbContext.CostCenters
                                                 .Where(ct => ct.StatusId == 1)
                                                 .AsNoTracking()
                                                 .ToListAsync(cancellationToken);

            var costCenterVms = _mapper.Map<List<CostCenterVm>>(costCenters);
            return costCenterVms.AsQueryable();
        }
    }
}
