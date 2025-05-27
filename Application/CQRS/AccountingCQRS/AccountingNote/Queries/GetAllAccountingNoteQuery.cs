using Application.Forms;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.AccountingCQRS.AccountingNote.Queries
{
    public class GetAllAccountingNoteQuery : IRequest<IQueryable<AccountingNoteFormVm>>
    {
    }
    public class GetAllAccountingNoteQueryHandler(IAppDbContext appDbContext, IMapper mapper, ILogger<GetAllAccountingNoteQueryHandler> logger) : IRequestHandler<GetAllAccountingNoteQuery, IQueryable<AccountingNoteFormVm>>
    {
        private IMapper _mapper { get; } = mapper;
        private IAppDbContext _context { get; } = appDbContext;

        public async Task<IQueryable<AccountingNoteFormVm>> Handle(GetAllAccountingNoteQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _context.AccountingNotes.Where(ct => ct.StatusId == 1).AsNoTracking().ToListAsync(cancellationToken);
            var result = queryResult.Select(x => _mapper.Map<AccountingNoteFormVm>(x)).AsQueryable();
            
            return result;
        }
    }
}