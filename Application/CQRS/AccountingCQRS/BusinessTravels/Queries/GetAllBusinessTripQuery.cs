using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Forms.Accounting;
using Application.Interfaces;

using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.BusinessTravels.Queries;

public class GetAllBusinessTripQuery : IRequest<IQueryable<BusinessTravelFormVm>>
{

}
public class GetAllBusinessTripQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetAllBusinessTripQuery, IQueryable<BusinessTravelFormVm>>
{
    private IMapper _mapper { get; } = mapper;
    private IAppDbContext _context { get; } = context ;


    

    public async Task<IQueryable<BusinessTravelFormVm>> Handle(GetAllBusinessTripQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _context.BusinessTravels.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var result = queryResult.Select(x => _mapper.Map<BusinessTravelFormVm>(x)).AsQueryable();

        return result;
    }
}

