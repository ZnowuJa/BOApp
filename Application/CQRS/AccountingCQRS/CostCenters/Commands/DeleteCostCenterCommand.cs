using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.CostCenters.Commands
{
    public class DeleteCostCenterCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DeleteCostCenterCommand(int id)
        {
            Id = id;
        }
    }
}
