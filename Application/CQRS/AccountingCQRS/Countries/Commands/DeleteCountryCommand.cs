using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.Countries.Commands
{
    public class DeleteCountryCommand(int id) : IRequest<int>
    {
        public int Id { get; set; } = id;
    }

    public class DeleteCountryCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteCountryCommand, int>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;

        public async Task<int> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.Countries
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new KeyNotFoundException($"Countries with Id {request.Id} not found.");

            _appDbContext.Countries.Remove(result);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return result.Id;
        }
    }
}
