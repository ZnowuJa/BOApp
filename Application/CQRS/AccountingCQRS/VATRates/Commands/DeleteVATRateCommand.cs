using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.VATRates.Commands
{
    public class DeleteVATRateCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteVATRateCommand(int id)
        {
            Id = id;
        }
    }
}
