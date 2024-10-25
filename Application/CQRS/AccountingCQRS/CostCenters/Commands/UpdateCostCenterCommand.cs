using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.CostCenters.Commands
{
    public class UpdateCostCenterCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string MPK { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }

        public UpdateCostCenterCommand(int id, string mpk, string description, string text)
        {
            Id = id;
            MPK = mpk;
            Description = description;
            Text = text;
        }
    }
}
