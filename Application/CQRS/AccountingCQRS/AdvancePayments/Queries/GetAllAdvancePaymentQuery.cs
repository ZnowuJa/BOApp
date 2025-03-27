using Application.Forms.Accounting;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.AdvancePayments.Queries
{
    public class GetAllAdvancePaymentQuery : IRequest<IQueryable<AdvancePaymentFormVm>>
    {
    }
    public class GetAllAdvancePaymentQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetAllAdvancePaymentQuery, IQueryable<AdvancePaymentFormVm>>
    {
        private IMapper _mapper { get; } = mapper;
        private IAppDbContext _context { get; } = context;

        public async Task<IQueryable<AdvancePaymentFormVm>> Handle(GetAllAdvancePaymentQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _context.AdvancePayments.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
            var result = queryResult.Select(x => _mapper.Map<AdvancePaymentFormVm>(x)).AsQueryable();

            return result;
        }
    }
}