using Application.Forms.Accounting;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.AdvancePayments.Queries
{
    public class GetAdvancePaymentByEmpIdQuery(int empId) : IRequest<IQueryable<AdvancePaymentFormVm>>
    {
        public int EmpId { get; set; } = empId;
    }

    public class GetAdvancePaymentByEmpIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetAdvancePaymentByEmpIdQuery, IQueryable<AdvancePaymentFormVm>>
    {
        public IAppDbContext _context { get; } = context;
        public IMapper _mapper { get; } = mapper;

        public async Task<IQueryable<AdvancePaymentFormVm>> Handle(GetAdvancePaymentByEmpIdQuery request, CancellationToken cancellationToken)
        {

            var queryResult = await _context.AdvancePayments.Where(ct => ct.StatusId == 1 && ct.EnovaEmpId == request.EmpId.ToString()).AsNoTracking().ToListAsync(cancellationToken);
            var result = queryResult.Select(x => _mapper.Map<AdvancePaymentFormVm>(x)).AsQueryable();

            return result;
        }
    }
}
