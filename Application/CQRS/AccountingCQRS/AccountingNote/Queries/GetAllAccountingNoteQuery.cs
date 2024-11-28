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
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger _logger = logger;

        public async Task<IQueryable<AccountingNoteFormVm>> Handle(GetAllAccountingNoteQuery request, CancellationToken cancellationToken)
        {
            var accountingNotes = await _appDbContext.AccountingNotes
                                                     .Where(ct => ct.StatusId == 1)
                                                     .AsNoTracking()
                                                     .ToListAsync(cancellationToken);
            var accountingNoteVms = _mapper.Map<List<AccountingNoteFormVm>>(accountingNotes);

            return accountingNoteVms.AsQueryable();
        }
    }
}