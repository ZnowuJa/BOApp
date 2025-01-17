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

public class GetBusinessTripByIdQuery(int id) : IRequest<BusinessTravelFormVm>
{
    public int Id = id;
}
public class GetBusinessTripByIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetBusinessTripByIdQuery, BusinessTravelFormVm>
{
    private IMapper _mapper { get; } = mapper;
    private IAppDbContext _context { get; } = context;




    public async Task<BusinessTravelFormVm> Handle(GetBusinessTripByIdQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _context.BusinessTravels.Where(ct => ct.StatusId == 1 && ct.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        var result = _mapper.Map<BusinessTravelFormVm>(queryResult);

        return result;
    }
}

