using Application.ViewModels.Settlement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.SettlementCQRS.Countries.Queries
{
    public class GetCountryQuery : IRequest<CountryVm>
    {
        public GetCountryQuery(int i)
        {
            CountryId = i;
        }
        public int CountryId { get; set; }
    }
}
