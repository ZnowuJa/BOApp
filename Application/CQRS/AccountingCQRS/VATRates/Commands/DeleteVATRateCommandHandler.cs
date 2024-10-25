using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.VATRates.Commands
{
    public class DeleteVATRateCommandHandler : IRequestHandler<DeleteVATRateCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        public DeleteVATRateCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Handle(DeleteVATRateCommand request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.VATRates
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new KeyNotFoundException($"VATRates with Id {request.Id} not found.");

            _appDbContext.VATRates.Remove(result);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return result.Id;
        }
    }
}
