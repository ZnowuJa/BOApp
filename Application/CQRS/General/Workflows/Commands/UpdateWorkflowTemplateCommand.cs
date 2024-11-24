using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;

namespace Application.CQRS.General.Workflows.Commands;
public class UpdateWorkflowTemplateCommand : IRequest<int>
{
    public WorkflowTemplateVm WorkflowTemplate { get; set; }

    public UpdateWorkflowTemplateCommand(WorkflowTemplateVm workflowTemplate)
    {
        WorkflowTemplate = workflowTemplate;
    }
}

public class UpdateWorkflowTemplateCommandHandler : IRequestHandler<UpdateWorkflowTemplateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public UpdateWorkflowTemplateCommandHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<int> Handle(UpdateWorkflowTemplateCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkflowTemplates.FindAsync(request.WorkflowTemplate.Id);

        if (entity == null)
        {
            //throw new NotFoundException(nameof(WorkflowTemplate), request.WorkflowTemplate.Id);
        }

        _mapper.Map(request.WorkflowTemplate, entity);

        await _context.SaveChangesAsync();

        return request.WorkflowTemplate.Id;
    }
}