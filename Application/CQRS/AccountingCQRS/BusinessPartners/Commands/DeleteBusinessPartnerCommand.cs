using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.BusinessPartners.Commands
{
    public class DeleteBusinessPartnerCommand(int id) : IRequest<int>
    {
        public int Id { get; set; } = id;
    }

    public class DeleteBusinessPartnerCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteBusinessPartnerCommand, int>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;

        public async Task<int> Handle(DeleteBusinessPartnerCommand request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.BusinessPartners
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new KeyNotFoundException($"BusinessPartner with Id {request.Id} not found.");

            _appDbContext.BusinessPartners.Remove(result);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return result.Id;
        }
    }
}