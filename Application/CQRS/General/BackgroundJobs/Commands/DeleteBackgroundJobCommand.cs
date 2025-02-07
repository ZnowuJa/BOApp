using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.BackgroundJobs.Commands
{
    public class DeleteBackgroundJobCommand(int id) : IRequest<int>
    {
        public int Id { get; set; } = id;
    }

    public class DeleteBackgroundJobCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteBackgroundJobCommand, int>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;

        public async Task<int> Handle(DeleteBackgroundJobCommand request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.BackgroundJobs
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new KeyNotFoundException($"BackgroundJob with Id {request.Id} not found.");

            _appDbContext.BackgroundJobs.Remove(result);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return result.Id;
        }
    }
}
