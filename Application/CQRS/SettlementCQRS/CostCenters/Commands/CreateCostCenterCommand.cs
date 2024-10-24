using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.SettlementCQRS.CostCenters.Commands
{
    public class CreateCostCenterCommand : IRequest<int>
    {
        public string MPK { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }

        public CreateCostCenterCommand(string mpk, string description, string text)
        {
            MPK = mpk;
            Description = description;
            Text = text;
        }
    }
}
