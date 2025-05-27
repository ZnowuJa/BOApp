using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.ViewModels.General;
using AutoMapper;
using MediatR;

namespace Application.CQRS.General.FormFiles.Queries;
public class GetFormFileByIdQuery : IRequest<FormFileVm>
{
    public int Id { get; set; }

    public GetFormFileByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetFormFileByIdQueryHandler : IRequestHandler<GetFormFileByIdQuery, FormFileVm>
{
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public GetFormFileByIdQueryHandler(IMapper mapper, IAppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<FormFileVm> Handle(GetFormFileByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.FormFiles.FindAsync(request.Id);

        return _mapper.Map<FormFileVm>(entity);
    }
}

