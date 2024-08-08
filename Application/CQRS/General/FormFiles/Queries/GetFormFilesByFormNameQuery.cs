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
public class GetFormFilesByFormNameQuery : IRequest<List<FormFileVm>>
{
    public string Name { get; set; }

    public GetFormFilesByFormNameQuery(string name)
    {
        Name = name;
    }
}

public class GetFormFilesByFormNameQueryHandler : IRequestHandler<GetFormFilesByFormNameQuery, List<FormFileVm>>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetFormFilesByFormNameQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<FormFileVm>> Handle(GetFormFilesByFormNameQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.FormFiles.Where(w => w.FormClassName == request.Name).ToListAsync(cancellationToken);

        return _mapper.Map<List<FormFileVm>>(entity);
    }
}

