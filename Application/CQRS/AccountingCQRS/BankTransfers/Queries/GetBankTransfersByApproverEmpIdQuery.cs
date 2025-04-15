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
public class GetBankTransfersByApproverEmpIdQuery(int empId) : IRequest<IQueryable<BankTransferFormVm>>
{
    public int EmpId { get; set; } = empId;
}

public class GetBankTransfersByApproverEmpIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetBankTransfersByApproverEmpIdQuery, IQueryable<BankTransferFormVm>>
{
    public IAppDbContext _context { get; } = context;
    public IMapper _mapper { get; } = mapper;

    public async Task<IQueryable<BankTransferFormVm>> Handle(GetBankTransfersByApproverEmpIdQuery request, CancellationToken cancellationToken)
    {

        var queryResult = await _context.BankTransfers.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        // var result =  _mapper.Map<BankTransferFormVm>(x);
        var result = queryResult.Select(x => _mapper.Map<BankTransferFormVm>(x)).AsQueryable();
        var finalResult = result
            .Where(x => x.LVL1_EnovaEmpId == request.EmpId.ToString() ||
                        x.LVL2_EnovaEmpId == request.EmpId.ToString() ||
                        x.LVL5_EnovaEmpId == request.EmpId.ToString()); //sprawdzić czy na pewno w przelewach jest
        
        return finalResult;

    }
}