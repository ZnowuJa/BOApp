using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.VATRates.Commands
{
    public class DeleteVATRateCommand(int id) : IRequest<int>
    {
        public int Id { get; set; } = id;
    }
    public class DeleteVATRateCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteVATRateCommand, int>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;

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
