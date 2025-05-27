using Application.CQRS.AccountingCQRS.CostCenters.Queries;
using Application.Interfaces;
using Application.ViewModels.Accounting;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.SapCostCenters.Queries;
public class GetSapCostCenterQuery(int i) : IRequest<SapCostCenterVm>
{
    public int CostCenterId { get; set; } = i;
}
public class GetSapCostCenterQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetSapCostCenterQuery, SapCostCenterVm>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<SapCostCenterVm> Handle(GetSapCostCenterQuery request, CancellationToken cancellationToken)
    {
        var costCenter = await _appDbContext.CostCenters
                                        .Where(p => p.Id == request.CostCenterId)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(cancellationToken);
        return _mapper.Map<SapCostCenterVm>(costCenter);
    }
}
