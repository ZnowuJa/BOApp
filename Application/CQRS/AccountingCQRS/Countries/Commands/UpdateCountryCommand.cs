using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.Countries.Commands
{
    public class UpdateCountryCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsEU { get; set; }

        public UpdateCountryCommand(int id, string code, string name, bool iseu)
        {
            Id = id;
            Code = code;
            Name = name;
            IsEU = iseu;
        }
    }
}
