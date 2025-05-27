using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.ManagerDeputies.Commands
{
    public class DeleteManagerDeputyCommand(int id) : IRequest<int>
    {
        public int Id { get; set; } = id;
    }

    public class DeleteManagerDeputyCommandHandler(IAppDbContext appDbContext) : IRequestHandler<DeleteManagerDeputyCommand, int>
    {
        private readonly IAppDbContext _appDbContext = appDbContext;

        public async Task<int> Handle(DeleteManagerDeputyCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.ManagerDeputies.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            _appDbContext.ManagerDeputies.Remove(item);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
    }
}