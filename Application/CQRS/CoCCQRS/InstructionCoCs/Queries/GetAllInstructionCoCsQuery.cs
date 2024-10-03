using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Application.ViewModels.CoC;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.CoCCQRS.InstructionCoCs.Queries;
public class GetAllInstructionCoCsQuery() : IRequest<IQueryable<InstructionCoCVm>>;

public class GetAllInstructionCoCsQueryHandler : IRequestHandler<GetAllInstructionCoCsQuery, IQueryable<InstructionCoCVm>>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllInstructionCoCsQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IQueryable<InstructionCoCVm>> Handle(GetAllInstructionCoCsQuery request, CancellationToken cancellationToken)
    {

        var instructions = await _context.Instructions.Include(i => i.Groups).ToListAsync(cancellationToken);
        var result = _mapper.Map<List<InstructionCoCVm>>(instructions).AsQueryable();

        return result;
    }
}
