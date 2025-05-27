using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms.Accounting;
using Application.Interfaces;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.BusinessTravels.Queries;
public class GetBusinessTripByCashierQuery(int empId) : IRequest<IQueryable<BusinessTravelFormVm>>
{
    public int EmpId { get; set; } = empId;
}

public class GetBusinessTripByCashierQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetBusinessTripByCashierQuery, IQueryable<BusinessTravelFormVm>>
{
    public IAppDbContext _context { get; } = context;
    public IMapper _mapper { get; } = mapper;

    public async Task<IQueryable<BusinessTravelFormVm>> Handle(GetBusinessTripByCashierQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _context.BusinessTravels.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var result = queryResult.Select(x => _mapper.Map<BusinessTravelFormVm>(x)).AsQueryable();
        var finalResult = result.Where(x => x.Level3Approvers.Any(approver => approver.EmpId == request.EmpId));

        return finalResult;
    }
}
