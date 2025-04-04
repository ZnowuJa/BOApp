using Application.Forms;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Accounting;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.CQRS.AccountingCQRS.AccountingNote.Commands
{
    public class CreateAccountingNoteCommand(AccountingNoteFormVm item) : IRequest<int> 
    {
        public AccountingNoteFormVm Item { get; set; } = item;
    }
    public class CreateAccountingNoteCommandHandler(IAppDbContext context, IMapper mapper, IConfiguration configuration) : IRequestHandler<CreateAccountingNoteCommand, int>
    {
        private readonly IAppDbContext _appDbContext = context;
        private readonly IMapper _mapper = mapper;
        private readonly IConfiguration _configuration = configuration;

        public async Task<int> Handle(CreateAccountingNoteCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<AccountingNoteForm>(request.Item);
            _appDbContext.AccountingNotes.Add(item);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            item.NoteNumber = $"{item.NumberPrefix}{item.Id:D8}";
            _appDbContext.AccountingNotes.Update(item);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}
