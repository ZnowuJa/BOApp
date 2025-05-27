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

namespace Application.CQRS.AccountingCQRS.CostAllocations.Queries;
public class GetAllCostAllocationsIEnumQuery : IRequest<IEnumerable<CostAllocationVm>>
{
}

public class GetAllCostAllocationsIEnumQueryyHandler(IAppDbContext appDbContext, IMapper mapper) : IRequestHandler<GetAllCostAllocationsIEnumQuery, IEnumerable<CostAllocationVm>>
{
    private readonly IAppDbContext _appDbContext = appDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<CostAllocationVm>> Handle(GetAllCostAllocationsIEnumQuery request, CancellationToken cancellationToken)
    {
        var costAllocations = await _appDbContext.CostAllocations
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var list = _mapper.Map<List<CostAllocationVm>>(costAllocations);
        return list;
    }
}