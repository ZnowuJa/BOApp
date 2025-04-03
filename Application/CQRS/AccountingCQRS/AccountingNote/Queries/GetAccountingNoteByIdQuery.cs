using Application.Forms;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.AccountingNote.Queries
{
    public class GetAccountingNoteByIdQuery(int i) : IRequest<AccountingNoteFormVm>
    {
        public int Id { get; set; } = i;
    }
    public class GetAccountingNoteByIdQueryHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetAccountingNoteByIdQuery, AccountingNoteFormVm>
    {
        private IMapper _mapper { get; } = mapper;
        private IAppDbContext _context { get; } = context;

        public async Task<AccountingNoteFormVm> Handle(GetAccountingNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _context.AccountingNotes.Where(ct => ct.StatusId == 1 && ct.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<AccountingNoteFormVm>(queryResult);

            return result;
        }
    }
}
