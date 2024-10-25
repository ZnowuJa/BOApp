using Application.ViewModels.Accounting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.CostCenters.Queries
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
