using Application.ViewModels.Settlement;
using Domain.Entities.Settlement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.SettlementCQRS.VATRates.Queries
{
    public class GetVATRateQuery : IRequest<VATRateVm>
    {
        public GetVATRateQuery(int i)
        {
            VATRateId = i;
        }
        public int VATRateId { get; set; }
    }
}
