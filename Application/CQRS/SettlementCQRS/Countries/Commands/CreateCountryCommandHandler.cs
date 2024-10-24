using Application.Interfaces;
using Domain.Entities.Settlement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.SettlementCQRS.Countries.Commands
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
    {
        private readonly IAppDbContext _appDbContext;

        public CreateCountryCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            Country country = new()
            {
                Code = request.Code,
                Name = request.Name,
                IsEU = request.IsEU,
                StatusId = 1
            };

            _appDbContext.Countries.Add(country);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return country.Id;
        }
    }
}
