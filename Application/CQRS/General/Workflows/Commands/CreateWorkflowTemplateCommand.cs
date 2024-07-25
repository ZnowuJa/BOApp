using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using Domain.WorkFlows;
using MediatR;

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.CQRS.General.Workflows.Commands;
public class CreateWorkflowTemplateCommand : IRequest<int>
{
    public WorkflowTemplateVm WorkflowTemplate { get; set; }

    public CreateWorkflowTemplateCommand(WorkflowTemplateVm workflowTemplate)
    {
        WorkflowTemplate = workflowTemplate;
    }
}

public class CreateWorkflowTemplateCommandHandler : IRequestHandler<CreateWorkflowTemplateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public CreateWorkflowTemplateCommandHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<int> Handle(CreateWorkflowTemplateCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<WorkflowTemplate>(request.WorkflowTemplate);

        _context.WorkflowTemplates.Add(entity);
        await _context.SaveChangesAsync();
        foreach(var e in entity.Steps)
        {
            e.WorkflowTemplateId = entity.Id;
        }
        await _context.SaveChangesAsync();
        return entity.Id;
    }
}
