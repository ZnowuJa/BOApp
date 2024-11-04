using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.CostCenters.Commands
{
    public class DeleteCostCenterCommand(int id) : IRequest<int>
    {
        public int Id { get; set; } = id;
    }
    public class DeleteCostCenterCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteCostCenterCommand, int>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;

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
