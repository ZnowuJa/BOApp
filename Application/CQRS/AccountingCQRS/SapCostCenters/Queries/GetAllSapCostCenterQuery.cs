using Application.Interfaces;
using Application.ViewModels.Accounting;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.SapCostCenters.Queries;
public class GetAllSapCostCenterQuery : IRequest<IQueryable<SapCostCenterVm>>
{
}

public class GetAllSapCostCenterQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllSapCostCenterQuery, IQueryable<SapCostCenterVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<IQueryable<SapCostCenterVm>> Handle(GetAllSapCostCenterQuery request, CancellationToken cancellationToken)
    {
        var costCenters = await _appDbContext.SapCostCenters
                                             .Where(ct => ct.StatusId == 1)
                                             .AsNoTracking()
                                             .ToListAsync(cancellationToken);

        var costCenterVms = _mapper.Map<List<SapCostCenterVm>>(costCenters);
        return costCenterVms.AsQueryable();
    }
}

