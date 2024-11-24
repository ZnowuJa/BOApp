using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;

using Domain.Entities.Common;
using Domain.WorkFlows;
using MediatR;

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.CQRS.General.FormFiles.Commands;
public class CreateFormFileCommand : IRequest<int>
{
    public FormFileVm FormFile { get; set; }

    public CreateFormFileCommand(FormFileVm formFile)
    {
        FormFile = formFile;
    }
}

public class CreateFormFileCommandHandler : IRequestHandler<CreateFormFileCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public CreateFormFileCommandHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<int> Handle(CreateFormFileCommand request, CancellationToken cancellationToken)
    {
        // var entity = new FormFile();
        // _mapper.Map(request.FormFile, entity);
        var entity = new FormFile()
        {
            
            TmpPath = request.FormFile.TmpPath,
            TmpFileName = request.FormFile.TmpFileName,
            TmpFileExtension = request.FormFile.TmpFileExtension,
            DstPath = request.FormFile.DstPath,
            DstFileName = request.FormFile.DstFileName,
            FormPurpose = request.FormFile.FormPurpose,
            Prefix = request.FormFile.Prefix,
            FolderName = request.FormFile.FolderName,
            FormClassName = request.FormFile.FormClassName,
            FormId = request.FormFile.FormId,
            Order = request.FormFile.Order,
            Deleted = request.FormFile.Deleted,
            OriginalFileName = request.FormFile.OriginalFileName,
        
        };

        _context.FormFiles.Add(entity);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.ToString());
        }
        

        return entity.Id;
    }
}
