using Application.Forms.Accounting;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.AdvancePayments.Queries
{
    public class GetAdvancePaymentByCashierQuery(int empId) : IRequest<IQueryable<AdvancePaymentFormVm>>
    {
        public int EmpId { get; set; } = empId;
    }

    public class GetAdvancePaymentByCashierQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetAdvancePaymentByCashierQuery, IQueryable<AdvancePaymentFormVm>>
    {
        public IAppDbContext _context { get; } = context;
        public IMapper _mapper { get; } = mapper;

        public async Task<IQueryable<AdvancePaymentFormVm>> Handle(GetAdvancePaymentByCashierQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _context.AdvancePayments.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
            var result = queryResult.Select(x => _mapper.Map<AdvancePaymentFormVm>(x)).AsQueryable();
            var finalResult = result.Where(x => x.Level3Approvers.Any(approver => approver.EmpId == request.EmpId));

            return finalResult;
        }
    }
}