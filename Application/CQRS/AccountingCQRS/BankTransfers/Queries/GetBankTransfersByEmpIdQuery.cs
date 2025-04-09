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

namespace Application.CQRS.AccountingCQRS.BankTransfers.Queries;
public class GetBankTransfersByEmpIdQuery(int empId) : IRequest<IQueryable<BankTransferFormVm>>
{
    public int EmpId { get; set; } = empId;
}
public class GetBankTransfersByEmpIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetBankTransfersByEmpIdQuery, IQueryable<BankTransferFormVm>>
{
    public IAppDbContext _context { get; } = context;
    public IMapper _mapper { get; } = mapper;

    public async Task<IQueryable<BankTransferFormVm>> Handle(GetBankTransfersByEmpIdQuery request, CancellationToken cancellationToken)
    {

        var queryResult = await _context.BankTransfers.Where(ct => ct.StatusId == 1 && ct.EnovaEmpId == request.EmpId.ToString()).AsNoTracking().ToListAsync(cancellationToken);
        var result = queryResult.Select(x => _mapper.Map<BankTransferFormVm>(x)).AsQueryable();

        return result;
    }
}