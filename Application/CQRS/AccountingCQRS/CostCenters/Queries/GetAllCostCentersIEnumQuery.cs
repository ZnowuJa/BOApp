using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.CostCenters.Queries
{
    public class GetAllCostCenterIEnumQuery : IRequest<IEnumerable<CostCenterVm>>
    {
    }

    public class GetAllCostCenterIEnumQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllCostCenterIEnumQuery, IEnumerable<CostCenterVm>>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<CostCenterVm>> Handle(GetAllCostCenterIEnumQuery request, CancellationToken cancellationToken)
        {
            var costCenters = await _appDbContext.CostCenters
                .Where(ct => ct.StatusId == 1)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var costCenterVms = _mapper.Map<List<CostCenterVm>>(costCenters).AsEnumerable();
            return costCenterVms;
        }
    }
}