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

namespace Application.CQRS.AccountingCQRS.BankTransfers.Queries;

public class GetAllBankTrasferQuery : IRequest<IQueryable<BankTransferFormVm>>
{

}
public class GetAllBankTrasfersQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetAllBankTrasferQuery, IQueryable<BankTransferFormVm>>
{
    private IMapper _mapper { get; } = mapper;
    private IAppDbContext _context { get; } = context ;

    public async Task<IQueryable<BankTransferFormVm>> Handle(GetAllBankTrasferQuery request, CancellationToken cancellationToken)
    {
        var queryResult = await _context.BankTransfers.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
        var result = queryResult.Select(x => _mapper.Map<BankTransferFormVm>(x)).AsQueryable();

        return result;
    }
}