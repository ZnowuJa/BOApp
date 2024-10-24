using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.SettlementCQRS.Countries.Commands
{
    public class CreateCountryCommand : IRequest<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsEU { get; set; }

        public CreateCountryCommand(string code, string name, bool isEU)
        {
            Code = code;
            Name = name;
            IsEU = isEU;
        }
    }
}
