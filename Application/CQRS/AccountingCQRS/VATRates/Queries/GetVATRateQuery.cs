using Application.ViewModels.Accounting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.VATRates.Queries
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
