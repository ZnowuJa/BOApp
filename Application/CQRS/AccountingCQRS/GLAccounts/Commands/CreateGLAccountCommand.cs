using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Commands
{
    public class CreateGLAccountCommand : IRequest<int>
    {
        public string AccountNumber { get; set; }
        public string Description { get; set; }

        public CreateGLAccountCommand(string accountNumber, string description)
        {
            AccountNumber = accountNumber;
            Description = description;
        }
    }
}
