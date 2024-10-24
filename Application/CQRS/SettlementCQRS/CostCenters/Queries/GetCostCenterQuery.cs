using Application.ViewModels.Settlement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.SettlementCQRS.CostCenters.Queries
{
    public class GetCostCenterQuery : IRequest<CostCenterVm>
    {
        public GetCostCenterQuery(int i) 
        {
            CostCenterId = i;
        }
        public int CostCenterId { get; set; }
    }
}
