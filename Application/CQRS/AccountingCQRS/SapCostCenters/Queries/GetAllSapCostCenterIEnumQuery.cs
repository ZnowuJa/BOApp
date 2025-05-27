using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.ViewModels.Accounting;
using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.SapCostCenters.Queries;
public class GetAllSapCostCenterIEnumQuery : IRequest<IEnumerable<SapCostCenterVm>>
{
}

public class GetAllSapCostCenterIEnumQueryHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllSapCostCenterIEnumQuery, IEnumerable<SapCostCenterVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<SapCostCenterVm>> Handle(GetAllSapCostCenterIEnumQuery request, CancellationToken cancellationToken)
    {
        var costCenters = await _appDbContext.SapCostCenters
            .Where(ct => ct.StatusId == 1)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var costCenterVms = _mapper.Map<List<SapCostCenterVm>>(costCenters);
        return costCenterVms;
    }
}