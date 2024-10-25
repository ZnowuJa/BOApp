using Application.ViewModels.Accounting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Queries
{
    public class GetGLAccountQuery : IRequest<GLAccountVm>
    {
        public GetGLAccountQuery(int i)
        {
            GLAccountId = i;
        }
        public int GLAccountId { get; set; }
    }
}
