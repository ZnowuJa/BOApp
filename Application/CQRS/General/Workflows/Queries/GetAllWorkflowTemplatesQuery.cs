using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.Workflows.Queries;
public class GetAllWorkflowTemplatesQuery : IRequest<List<WorkflowTemplateVm>>
{
}

public class GetAllWorkflowTemplatesQueryHandler : IRequestHandler<GetAllWorkflowTemplatesQuery, List<WorkflowTemplateVm>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetAllWorkflowTemplatesQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<WorkflowTemplateVm>> Handle(GetAllWorkflowTemplatesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.WorkflowTemplates.ToListAsync();

        return _mapper.Map<List<WorkflowTemplateVm>>(entities);
    }
}