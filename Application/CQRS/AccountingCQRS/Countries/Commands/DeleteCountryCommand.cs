using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.Countries.Commands
{
    public class DeleteCountryCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteCountryCommand(int id)
        {
            Id = id;
        }
    }
}
