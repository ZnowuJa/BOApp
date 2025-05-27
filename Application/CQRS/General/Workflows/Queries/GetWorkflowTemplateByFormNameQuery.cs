using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Workflows.Queries;
public class GetWorkflowTemplateByFormNameQuery : IRequest<WorkflowTemplateVm>
{
    public string Name { get; set; }

    public GetWorkflowTemplateByFormNameQuery(string name)
    {
        Name = name;
    }
}

public class GetWorkflowTemplateByFormNameQueryHandler : IRequestHandler<GetWorkflowTemplateByFormNameQuery, WorkflowTemplateVm>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetWorkflowTemplateByFormNameQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<WorkflowTemplateVm> Handle(GetWorkflowTemplateByFormNameQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkflowTemplates.Where(w => w.FormClassName == request.Name).FirstOrDefaultAsync(cancellationToken);
        var steps = await _context.WorkflowSteps.Where(s => s.WorkflowTemplateId == entity.Id).ToListAsync();
        entity.Steps = steps;
        return _mapper.Map<WorkflowTemplateVm>(entity);
    }
}

