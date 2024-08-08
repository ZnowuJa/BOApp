using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Application.Interfaces;

namespace Application.CQRS.General.FormFiles.Commands
{
    public class DeleteFormFileCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteFormFileCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteFormFileCommandHandler : IRequestHandler<DeleteFormFileCommand, int>
    {
        private readonly IAppDbContext _context;

        public DeleteFormFileCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteFormFileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.FormFiles.FindAsync(request.Id);

            if (entity == null)
            {
                throw new Exception($"Entity with Id {request.Id} not found.");
            }

            _context.FormFiles.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}