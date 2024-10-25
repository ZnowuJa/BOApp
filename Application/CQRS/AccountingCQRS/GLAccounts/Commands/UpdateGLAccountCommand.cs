using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Commands
{
    public class UpdateGLAccountCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }

        public UpdateGLAccountCommand(int id, string accountNumber, string description)
        {
            Id = id;
            AccountNumber = accountNumber;
            Description = description;
        }
    }
}
