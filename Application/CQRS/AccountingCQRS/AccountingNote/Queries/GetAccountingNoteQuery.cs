using Application.Forms;
using Application.ViewModels.Accounting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.AccountingNote.Queries
{
    public class GetAccountingNoteQuery(int i) : IRequest<AccountingNoteFormVm>
    {
        public int Id { get; set; } = i;
    }
}
