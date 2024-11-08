using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.CostCenters.Queries
{
    public class GetAllCostCenterQuery : IRequest<IQueryable<CostCenterVm>>
    {
    }

    public class GetAllCostCenterQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllCostCenterQuery, IQueryable<CostCenterVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

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
