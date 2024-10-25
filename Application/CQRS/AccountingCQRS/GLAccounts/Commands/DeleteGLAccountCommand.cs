using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Commands
{
    public class DeleteGLAccountCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteGLAccountCommand(int id)
        {
            Id = id;
        }
    }
}
