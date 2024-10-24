using Application.ViewModels.Settlement;
using Domain.Entities.Settlement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.SettlementCQRS.GLAccounts.Queries
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
