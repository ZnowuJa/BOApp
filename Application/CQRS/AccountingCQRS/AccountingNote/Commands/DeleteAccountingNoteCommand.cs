using Application.CQRS.AccountingCQRS.AdvancePayments.Commands;
using Application.Forms;
using Application.Forms.Accounting;
using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.AccountingNote.Commands
{
    public class DeleteAccountingNoteCommand(AccountingNoteFormVm item) : IRequest<int>
    {
        public AccountingNoteFormVm Item { get; set; } = item;
    }
    public class DeleteAccountingNoteCommandHandler(IAppDbContext context) : IRequestHandler<DeleteAccountingNoteCommand, int>
    {
        private readonly IAppDbContext _appDbContext = context;

        public async Task<int> Handle(DeleteAccountingNoteCommand request, CancellationToken cancellationToken)
        {
            var item = _appDbContext.AccountingNotes.Where(b => b.Id == request.Item.Id).First() ?? throw new KeyNotFoundException($"Accounting Note Form with Id {request.Item.Id} not found.");
            _appDbContext.AccountingNotes.Remove(item);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return item.Id;
        }
    }
}