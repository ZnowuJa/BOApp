using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.VATRates.Commands
{
    public class CreateVATRateCommand : IRequest<int>
    {
        public string Name { get; set; }
        public double Percentage { get; set; }
        public string Information { get; set; }
        public int Order { get; set; }

        public CreateVATRateCommand(string name, double percentage, string information, int order)
        {
            Name = name;
            Percentage = percentage;
            Information = information;
            Order = order;
        }
    }
}
