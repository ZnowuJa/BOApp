using Application.Interfaces;
using Domain.Entities.Settlement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.SettlementCQRS.VATRates.Commands
{
    public class CreateVATRateCommandHandler : IRequestHandler<CreateVATRateCommand, int>
    {
        private readonly IAppDbContext _appDbContext;

        public CreateVATRateCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> Handle(CreateVATRateCommand request, CancellationToken cancellationToken)
        {
            VATRate vatRate = new()
            {
                Name = request.Name,
                Percentage = request.Percentage,
                Information = request.Information,
                Order = request.Order,
                StatusId = 1
            };

            _appDbContext.VATRates.Add(vatRate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return vatRate.Id;
        }
    }
}
