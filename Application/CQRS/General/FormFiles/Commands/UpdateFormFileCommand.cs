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

namespace Application.CQRS.General.FormFiles.Commands;
public class UpdateFormFileCommand : IRequest<int>
{
    public FormFileVm FormFile { get; set; }

    public UpdateFormFileCommand(FormFileVm formFile)
    {
        FormFile = formFile;
    }
}

public class UpdateFormFileCommandHandler : IRequestHandler<UpdateFormFileCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public UpdateFormFileCommandHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<int> Handle(UpdateFormFileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.FormFiles.FindAsync(request.FormFile.Id);

        if (entity == null)
        {
            //throw new NotFoundException(nameof(WorkflowTemplate), request.WorkflowTemplate.Id);
        }

        _mapper.Map(request.FormFile, entity);

        await _context.SaveChangesAsync();

        return request.FormFile.Id;
    }
}