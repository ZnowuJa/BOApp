using MediatR;
using Application.Interfaces;

namespace Application.CQRS.General.Workflows.Commands
{
    public class DeleteWorkflowTemplateCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteWorkflowTemplateCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteWorkflowTemplateCommandHandler : IRequestHandler<DeleteWorkflowTemplateCommand, int>
    {
        private readonly IAppDbContext _context;

        public DeleteWorkflowTemplateCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteWorkflowTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.WorkflowTemplates.FindAsync(request.Id);

            if (entity == null)
            {
                throw new Exception($"Entity with Id {request.Id} not found.");
            }

            _context.WorkflowTemplates.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}