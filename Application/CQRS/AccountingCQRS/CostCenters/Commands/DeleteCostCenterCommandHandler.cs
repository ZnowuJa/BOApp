using Application.Interfaces;
using Application.ITWarehouseCQRS.Invoices.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AccountingCQRS.CostCenters.Commands
{
    public class DeleteCostCenterCommandHandler : IRequestHandler<DeleteCostCenterCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        public DeleteCostCenterCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Handle(DeleteCostCenterCommand request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.CostCenters
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new KeyNotFoundException($"CostCenter with Id {request.Id} not found.");

            _appDbContext.CostCenters.Remove(result);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return result.Id;
        }
    }
}
