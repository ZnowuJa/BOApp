using Application.Forms.Accounting;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.AdvancePayments.Queries
{
    public class GetAdvancePaymentByIdQuery(int id) : IRequest<AdvancePaymentFormVm>
    {
        public int Id = id;
    }
    public class GetAdvancePaymentByIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetAdvancePaymentByIdQuery, AdvancePaymentFormVm>
    {
        private IMapper _mapper { get; } = mapper;
        private IAppDbContext _context { get; } = context;

        public async Task<AdvancePaymentFormVm> Handle(GetAdvancePaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _context.AdvancePayments.Where(ct => ct.StatusId == 1 && ct.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<AdvancePaymentFormVm>(queryResult);

            return result;
        }
    }
}
