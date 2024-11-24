using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Workflows.Queries;
public class GetWorkflowStepsByTemplateIdQuery : IRequest<List<WorkflowStepVm>>
{
    public int Id { get; set; }

    public GetWorkflowStepsByTemplateIdQuery(int id)
    {
        Id = id;
    }
}

public class GetWorkflowStepsByTemplateIdQueryHandler : IRequestHandler<GetWorkflowStepsByTemplateIdQuery, List<WorkflowStepVm>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetWorkflowStepsByTemplateIdQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<WorkflowStepVm>> Handle(GetWorkflowStepsByTemplateIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkflowSteps.Where(ws => ws.WorkflowTemplateId == request.Id).ToListAsync(cancellationToken);

        return _mapper.Map<List<WorkflowStepVm>>(entity);
    }
}

