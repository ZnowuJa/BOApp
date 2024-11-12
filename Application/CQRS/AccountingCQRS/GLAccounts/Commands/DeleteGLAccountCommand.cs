using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.AccountingCQRS.GLAccounts.Commands
{
    public class DeleteGLAccountCommand(int id) : IRequest<int>
    {
        public int Id { get; set; } = id;
    }
    public class DeleteGLAccountCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteGLAccountCommand, int>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;

        public async Task<int> Handle(DeleteGLAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.GLAccounts
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new KeyNotFoundException($"GLAccounts with Id {request.Id} not found.");

            _appDbContext.GLAccounts.Remove(result);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return result.Id;
        }
    }
}
