using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;

namespace Application.CQRS.General.Workflows.Queries;
public class GetWorkflowTemplateByIdQuery : IRequest<WorkflowTemplateVm>
{
    public int Id { get; set; }

    public GetWorkflowTemplateByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetWorkflowTemplateByIdQueryHandler : IRequestHandler<GetWorkflowTemplateByIdQuery, WorkflowTemplateVm>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetWorkflowTemplateByIdQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<WorkflowTemplateVm> Handle(GetWorkflowTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkflowTemplates.FindAsync(request.Id);

        return _mapper.Map<WorkflowTemplateVm>(entity);
    }
}

