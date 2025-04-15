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
public class GetBankTransfersByIdQuery(int id) : IRequest<BankTransferFormVm>
{
    public int Id { get; set; } = id;
}
public class GetBankTransfersByIdQueryQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetBankTransfersByIdQuery, BankTransferFormVm>
{
    public IAppDbContext _context { get; } = context;
    public IMapper _mapper { get; } = mapper;

    public async Task<BankTransferFormVm> Handle(GetBankTransfersByIdQuery request, CancellationToken cancellationToken)
    {

        var x = await _context.BankTransfers.Where(ct => ct.StatusId == 1 && ct.Id == request.Id).AsNoTracking().ToListAsync(cancellationToken);
        var result =  _mapper.Map<BankTransferFormVm>(x);

        return result;
    }
}