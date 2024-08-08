using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.General.FormFiles.Queries;
public class GetAllFormFilesQuery : IRequest<List<FormFileVm>>
{
}

public class GetAllFormFilesQueryHandler : IRequestHandler<GetAllFormFilesQuery, List<FormFileVm>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetAllFormFilesQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<FormFileVm>> Handle(GetAllFormFilesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.FormFiles.ToListAsync();

        return _mapper.Map<List<FormFileVm>>(entities);
    }
}