using Application.Forms;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.CQRS.AccountingCQRS.AccountingNote.Commands
{
    public class UpdateAccountingNoteCommand(AccountingNoteFormVm item) : IRequest<int>
    {
        public AccountingNoteFormVm Item { get; set; } = item;
    }

    public class UpdateAccountingNoteCommandHandler(IAppDbContext appDbContext, IMapper mapper, IConfiguration configuration) : IRequestHandler<UpdateAccountingNoteCommand, int>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;
        public async Task<int> Handle(UpdateAccountingNoteCommand request, CancellationToken cancellationToken)
        {
            var accNotes = await _appDbContext.AccountingNotes.FirstOrDefaultAsync(v => v.Id == request.Item.Id, cancellationToken);
            _mapper.Map(request.Item, accNotes);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return accNotes.Id;
        }
    }
}
